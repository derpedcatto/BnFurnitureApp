using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.DTO.Response;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.CategoryController.Queries;

public sealed record GetAllSubCategoriesQuery(
    string CategorySlug,
    bool IncludeImages,
    int? PageNumber,
    int? PageSize);

public sealed class GetAllSubCategoriesResponse
{
    public List<ProductCategoryDTO>? SubCategories { get; set; }

    public GetAllSubCategoriesResponse(List<ProductCategoryDTO>? productCategories)
    {
        SubCategories = productCategories;
    }
}

public sealed class GetAllSubCategoriesHandler : QueryHandler<GetAllSubCategoriesQuery, GetAllSubCategoriesResponse>
{
    private readonly IAppImageService _appImageService;

    public GetAllSubCategoriesHandler(
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _appImageService = appImageService;
    }

    public override async Task<ApiQueryResponse<GetAllSubCategoriesResponse>> Handle(
            GetAllSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<ProductCategory> query = HandlerContext.DbContext.ProductCategory
            .Include(c => c.ParentCategory)
            .Where(c => c.ParentCategory.Slug == request.CategorySlug)
            .OrderBy(c => c.Name);

        if (request.PageNumber.HasValue && request.PageSize.HasValue)
        {
            int skip = (request.PageNumber.Value - 1) * request.PageSize.Value;
            query = query.Skip(skip).Take(request.PageSize.Value);
        }

        var categories = await query.ToListAsync(cancellationToken);

        if (categories == null)
        {
            return new ApiQueryResponse<GetAllSubCategoriesResponse>
                (true, (int)HttpStatusCode.OK)
            {
                Data = null,
            };
        }

        var categoriesDTOList = await MapCategoriesToDTOs(
            categories,
            request.IncludeImages,
            true,
            cancellationToken);

        return new ApiQueryResponse<GetAllSubCategoriesResponse>
            (true, (int)HttpStatusCode.OK)
        {
            Data = new(categoriesDTOList),
        };
    }

    private async Task<List<ProductCategoryDTO>> MapCategoriesToDTOs(
        List<ProductCategory> categories,
        bool includeImages,
        bool flatList,
        CancellationToken cancellationToken)
    {
        var categoryDictionary = categories.ToDictionary(c => c.Id);
        var dtoDictionary = new Dictionary<Guid, ProductCategoryDTO>();

        foreach (var category in categories)
        {
            if (!dtoDictionary.ContainsKey(category.Id))
            {
                dtoDictionary[category.Id] = await MapCategoryToDTOAsync(
                    category, includeImages, flatList, cancellationToken);
            }

            if (!flatList &&
                category.ParentId.HasValue &&
                categoryDictionary.ContainsKey(category.ParentId.Value))
            {
                if (!dtoDictionary.ContainsKey(category.ParentId.Value))
                {
                    dtoDictionary[category.ParentId.Value] = await MapCategoryToDTOAsync(
                        categoryDictionary[category.ParentId.Value], includeImages, flatList, cancellationToken);
                }

                dtoDictionary[category.ParentId.Value].SubCategories ??= new();
                dtoDictionary[category.ParentId.Value].SubCategories!.Add(dtoDictionary[category.Id]);
            }
        }

        if (flatList)
        {
            return dtoDictionary.Values.ToList();
        }
        else
        {
            var rootCategoryDTOs = dtoDictionary.Values
                .Where(dto => !dtoDictionary.Values
                    .Any(parentDto => parentDto.SubCategories?
                        .Any(sub => sub.Id == dto.Id) == true))
                .ToList();

            return rootCategoryDTOs;
        }
    }

    private async Task<ProductCategoryDTO> MapCategoryToDTOAsync(
        ProductCategory category,
        bool includeImages,
        bool flatList,
        CancellationToken cancellationToken)
    {
        var imageUri = string.Empty;

        if (includeImages)
        {
            var imageResponse = await _appImageService.GetImagesAsync(
                AppEntityType.ProductCategory,
                category.Id,
                AppEntityImageType.PromoCardThumbnail,
                cancellationToken);

            imageUri = imageResponse.Data?.LastOrDefault() ?? string.Empty;
        }

        return new ProductCategoryDTO
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