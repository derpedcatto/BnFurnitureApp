using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductArticleController.DTO.Response;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductArticleController.Queries;

public sealed record GetProductArticlesByCategoryQuery(
    string CategorySlug,
    int PageNumber,
    int PageSize);

public sealed class GetProductArticlesByCategoryResponse
{
    public List<ProductArticleDTO> Articles { get; set; }
    public int TotalCount { get; private set; }
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }

    public GetProductArticlesByCategoryResponse(List<ProductArticleDTO> articles, int totalCount, int pageNumber, int pageSize)
    {
        Articles = articles;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

public sealed class GetProductArticlesByCategoryHandler : QueryHandler<GetProductArticlesByCategoryQuery, GetProductArticlesByCategoryResponse>
{
    private readonly IAppImageService _appImageService;

    public GetProductArticlesByCategoryHandler(
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _appImageService = appImageService;
    }

    public override async Task<ApiQueryResponse<GetProductArticlesByCategoryResponse>> Handle(
        GetProductArticlesByCategoryQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the root category ID based on slug
        var rootCategory = await HandlerContext.DbContext.ProductCategory
            .FirstOrDefaultAsync(c => c.Slug == request.CategorySlug, cancellationToken);

        if (rootCategory == null)
        {
            return new ApiQueryResponse<GetProductArticlesByCategoryResponse>(false, (int)HttpStatusCode.NotFound);
        }

        // Retrieve all related category IDs
        var allCategoryIds = await GetAllCategoryIdsIncludingChildren(rootCategory.Id, cancellationToken);

        // Get the total count of product articles
        var totalArticlesCount = await HandlerContext.DbContext.ProductArticle
            .CountAsync(pa => allCategoryIds.Contains(pa.Product.ProductType.CategoryId) && pa.Active, cancellationToken);

        // Fetch products from these categories with pagination
        var productArticles = await HandlerContext.DbContext.ProductArticle
            .Include(pa => pa.Product)
                .ThenInclude(p => p.ProductType)
                .ThenInclude(pt => pt.ProductCategory)
            .Where(pa => allCategoryIds.Contains(pa.Product.ProductType.CategoryId) && pa.Active)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var productArticleDTOList = MapProductArticlesToDTOs(productArticles);

        foreach (var productArticle in productArticleDTOList)
        {
            var thumbnailResult = await _appImageService.GetImagesAsync(
                Domain.Enums.AppEntityType.Product,
                productArticle.ProductId,
                Domain.Enums.AppEntityImageType.Thumbnail,
                cancellationToken);

            productArticle.ThumbnailImageUri = thumbnailResult.Data.FirstOrDefault();
        }

        var responseData = new GetProductArticlesByCategoryResponse(
            productArticleDTOList,
            totalArticlesCount,
            request.PageNumber,
            request.PageSize);

        return new ApiQueryResponse<GetProductArticlesByCategoryResponse>(true, (int)HttpStatusCode.OK)
        {
            Data = responseData
        };
    }

    private async Task<List<Guid>> GetAllCategoryIdsIncludingChildren(Guid parentId, CancellationToken cancellationToken)
    {
        var allCategories = await HandlerContext.DbContext.ProductCategory
            .ToListAsync(cancellationToken);

        var allIds = new List<Guid>();
        void AddIdsRecursive(Guid id)
        {
            allIds.Add(id);
            var childIds = allCategories.Where(c => c.ParentId == id).Select(c => c.Id);
            foreach (var childId in childIds)
            {
                AddIdsRecursive(childId);
            }
        }

        AddIdsRecursive(parentId);
        return allIds;
    }

    private List<ProductArticleDTO> MapProductArticlesToDTOs(List<ProductArticle> productArticles)
    {
        return productArticles.Select(pa => new ProductArticleDTO
        {
            Article = pa.Article,
            ProductId = pa.ProductId,
            AuthorId = pa.AuthorId,
            Name = pa.Name,
            CreatedAt = pa.CreatedAt,
            UpdatedAt = pa.UpdatedAt,
            Price = pa.Price,
            Discount = pa.Discount,
            Active = pa.Active,
            ProductTypeName = pa.Product.ProductType.Name,
        }).ToList();
    }
}
