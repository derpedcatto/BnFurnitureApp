using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.DTO.Response;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.CategoryController.Queries;

public sealed record GetAllCategoriesQuery(
    bool IncludeImages,
    bool FlatList,
    bool RandomOrder,
    int? PageNumber,
    int? PageSize);

public sealed class GetAllCategoriesResponse
{
    public int TotalCount { get; private set; }
    public List<ProductCategoryDTO> Categories { get; private set; }

    public GetAllCategoriesResponse(List<ProductCategoryDTO> categories, int totalCount)
    {
        Categories = categories;
        TotalCount = totalCount;
    }
} 

public sealed class GetAllCategoriesHandler : QueryHandler<GetAllCategoriesQuery, GetAllCategoriesResponse>
{
    private readonly IAppImageService _appImageService;

    public GetAllCategoriesHandler(
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _appImageService = appImageService;
    }

    public override async Task<ApiQueryResponse<GetAllCategoriesResponse>> Handle(
        GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var totalCategoriesCount = await HandlerContext.DbContext.ProductCategory
            .CountAsync(cancellationToken);

        IQueryable<ProductCategory> query = HandlerContext.DbContext.ProductCategory
            .Include(c => c.ParentCategory)
            .OrderBy(c => c.Name);

        if (request.PageNumber.HasValue && request.PageSize.HasValue)
        {
            int skip = (request.PageNumber.Value - 1) * request.PageSize.Value;
            query = query.Skip(skip).Take(request.PageSize.Value);
        }

        var categories = await query.ToListAsync(cancellationToken);

        if (request.RandomOrder)
        {
            categories = categories.OrderBy(_ => Random.Shared.Next()).ToList();
        }

        var categoriesDTOList = await MapCategoriesToDTOs(
            categories,
            request.IncludeImages,
            request.FlatList,
            cancellationToken);

        var responseData = new GetAllCategoriesResponse(categoriesDTOList, totalCategoriesCount);
        return new ApiQueryResponse<GetAllCategoriesResponse>
            (true, (int)HttpStatusCode.OK)
        {
            Data = responseData
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
                    category, includeImages, cancellationToken);
            }

            if (!flatList &&
                category.ParentId.HasValue && 
                categoryDictionary.ContainsKey(category.ParentId.Value))
            {
                if (!dtoDictionary.ContainsKey(category.ParentId.Value))
                {
                    dtoDictionary[category.ParentId.Value] = await MapCategoryToDTOAsync(
                        categoryDictionary[category.ParentId.Value], includeImages, cancellationToken);
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