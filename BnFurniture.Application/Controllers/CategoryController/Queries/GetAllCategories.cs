using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.DTO;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.CategoryController.Queries;

public sealed record GetAllCategoriesQuery();

public sealed class GetAllCategoriesResponse
{
    public List<ResponseProductCategoryDTO> Categories { get; private set; }

    public GetAllCategoriesResponse(List<ResponseProductCategoryDTO> categories)
    {
        Categories = categories;
    }
}

public sealed class GetAllCategoriesHandler : QueryHandler<GetAllCategoriesQuery, GetAllCategoriesResponse>
{
    private readonly IAppImageService _appImageService;

    public GetAllCategoriesHandler(IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _appImageService = appImageService;
    }

    public override async Task<ApiQueryResponse<GetAllCategoriesResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var dbContext = HandlerContext.DbContext;

        var categories = await dbContext.ProductCategory
            .Include(c => c.ParentCategory)
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);

        var categoryDTOList = await MapCategoriesToDTOs(categories, cancellationToken);

        var responseData = new GetAllCategoriesResponse(categoryDTOList);
        return new ApiQueryResponse<GetAllCategoriesResponse>(true, (int)HttpStatusCode.OK)
        {
            Data = responseData
        };
    }

    private async Task<List<ResponseProductCategoryDTO>> MapCategoriesToDTOs(List<ProductCategory> categories, CancellationToken cancellationToken)
    {
        var categoryDictionary = categories.ToDictionary(c => c.Id);
        var dtoDictionary = new Dictionary<Guid, ResponseProductCategoryDTO>();

        foreach (var category in categories)
        {
            if (!dtoDictionary.ContainsKey(category.Id))
            {
                dtoDictionary[category.Id] = await MapCategoryToDTOAsync(category, cancellationToken);
            }

            if (category.ParentId.HasValue)
            {
                if (!dtoDictionary.ContainsKey(category.ParentId.Value))
                {
                    dtoDictionary[category.ParentId.Value] = await MapCategoryToDTOAsync(categoryDictionary[category.ParentId.Value], cancellationToken);
                }

                if (dtoDictionary[category.ParentId.Value].SubCategories == null)
                {
                    dtoDictionary[category.ParentId.Value].SubCategories = [];
                }

                dtoDictionary[category.ParentId.Value].SubCategories.Add(dtoDictionary[category.Id]);
            }
        }

        return dtoDictionary.Values
            .Where(dto => !categories
                .Any(c => c.Id == dto.Id && c.ParentId.HasValue))
            .ToList();
    }

    private async Task<ResponseProductCategoryDTO> MapCategoryToDTOAsync(ProductCategory category, CancellationToken cancellationToken)
    {
        var imageResponse = await _appImageService.GetImagesAsync(
            AppEntityType.ProductCategory,
            category.Id,
            AppEntityImageType.PromoCardThumbnail,
            cancellationToken);

        var imageUri = imageResponse.Data?.LastOrDefault() ?? string.Empty;

        return new ResponseProductCategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Priority = category.Priority,
            CardImageUri = imageUri,
            SubCategories = null
        };
    }
}