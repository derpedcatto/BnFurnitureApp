using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.DTO;
using BnFurniture.Application.Controllers.ProductTypeController.DTO;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace BnFurniture.Application.Controllers.CategoryController.Queries;

public sealed record GetAllCategoriesWithProductTypesQuery();

public sealed class GetAllCategoriesWithProductTypesResponse 
{
    public List<ResponseProductCategoryWithProductTypesDTO> Categories { get; set; }

    public GetAllCategoriesWithProductTypesResponse(List<ResponseProductCategoryWithProductTypesDTO> categories)
    {
        Categories = categories;
    }
}

public sealed class GetAllCategoriesWithProductTypesHandler : QueryHandler<GetAllCategoriesWithProductTypesQuery, GetAllCategoriesWithProductTypesResponse>
{
    private readonly GetAllCategoriesHandler _getAllCategoriesHandler;
    private readonly IAppImageService _appImageService;

    public GetAllCategoriesWithProductTypesHandler(GetAllCategoriesHandler getAllCategoriesHandler,
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _getAllCategoriesHandler = getAllCategoriesHandler;
        _appImageService = appImageService;
    }

    public override async Task<ApiQueryResponse<GetAllCategoriesWithProductTypesResponse>> Handle(GetAllCategoriesWithProductTypesQuery request, CancellationToken cancellationToken)
    {
        var categoriesResponse = await _getAllCategoriesHandler.Handle(new(), cancellationToken);
        if (!categoriesResponse.IsSuccess)
        {
            return new ApiQueryResponse<GetAllCategoriesWithProductTypesResponse>(false, categoriesResponse.StatusCode)
            {
                Message = categoriesResponse.Message,
                Errors = categoriesResponse.Errors,
                Data = null
            };
        }

        var categories = categoriesResponse.Data!.Categories;

        var categoriesWithProductTypes = await PopulateCategoriesWithProductTypes(categories, cancellationToken);

        return new ApiQueryResponse<GetAllCategoriesWithProductTypesResponse>(true, 200)
        {
            Data = new(categoriesWithProductTypes)
        };
    }

    private async Task<List<ResponseProductCategoryWithProductTypesDTO>> PopulateCategoriesWithProductTypes(List<ResponseProductCategoryDTO> categories, CancellationToken cancellationToken)
    {
        var populatedCategories = new List<ResponseProductCategoryWithProductTypesDTO>();

        foreach (var category in categories)
        {
            var populatedCategory = new ResponseProductCategoryWithProductTypesDTO
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                Priority = category.Priority,
                CardImageUri = category.CardImageUri,
                ProductTypes = await GetProductTypesForCategory(category.Id, cancellationToken),
            };

            if (category.SubCategories != null && category.SubCategories.Count != 0)
            {
                populatedCategory.SubCategories = await PopulateCategoriesWithProductTypes(category.SubCategories, cancellationToken);
            }

            populatedCategories.Add(populatedCategory);
        }

        return populatedCategories;
    }

    private async Task<List<ResponseProductTypeDTO>> GetProductTypesForCategory(Guid categoryId, CancellationToken cancellationToken)
    {
        var productTypes = await HandlerContext.DbContext.ProductType
            .Where(pt => pt.CategoryId == categoryId)
            .Select(pt => new ResponseProductTypeDTO
            {
                Id = pt.Id,
                CategoryId = pt.CategoryId,
                Name = pt.Name,
                Slug = pt.Slug,
                Priority = pt.Priority,
                CardImageUri = string.Empty
            })
            .OrderBy(pt => pt.Name)
            .ToListAsync(cancellationToken);

        foreach (var productType in productTypes)
        {
            var imageResponse = await _appImageService.GetImagesAsync(
                AppEntityType.ProductType,
                productType.Id,
                AppEntityImageType.PromoCardThumbnail,
                cancellationToken);

            productType.CardImageUri = imageResponse.Data?.LastOrDefault() ?? string.Empty;
        }

        return productTypes;
    }
}