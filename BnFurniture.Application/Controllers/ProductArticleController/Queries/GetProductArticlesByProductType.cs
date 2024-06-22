using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductArticleController.DTO.Response;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductArticleController.Queries;

public sealed record GetProductArticlesByProductTypeQuery(
    string CategorySlug,
    string ProductTypeSlug,
    int PageNumber,
    int PageSize);


public sealed class GetProductArticlesByProductTypeResponse
{
    public List<ProductArticleDTO> Articles { get; set; }
    public int TotalCount { get; private set; }
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }

    public GetProductArticlesByProductTypeResponse(List<ProductArticleDTO> articles, int totalCount, int pageNumber, int pageSize)
    {
        Articles = articles;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

public sealed class GetProductArticlesByProductTypeHandler : QueryHandler<GetProductArticlesByProductTypeQuery, GetProductArticlesByProductTypeResponse>
{
    private readonly IAppImageService _appImageService;

    public GetProductArticlesByProductTypeHandler(
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _appImageService = appImageService;
    }

    public override async Task<ApiQueryResponse<GetProductArticlesByProductTypeResponse>> Handle(
        GetProductArticlesByProductTypeQuery request, CancellationToken cancellationToken)
    {
        var totalArticlesCount = await HandlerContext.DbContext.ProductArticle
            .CountAsync(pa => pa.Product.ProductType.Slug == request.ProductTypeSlug
                              && pa.Product.ProductType.ProductCategory.Slug == request.CategorySlug
                              && pa.Active,
                        cancellationToken);

        var productArticles = await HandlerContext.DbContext.ProductArticle
            .Include(pa => pa.Product)
                .ThenInclude(p => p.ProductType)
                .ThenInclude(pt => pt.ProductCategory)
            .Where(pa => pa.Product.ProductType.Slug == request.ProductTypeSlug
                         && pa.Product.ProductType.ProductCategory.Slug == request.CategorySlug
                         && pa.Active)
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

        var responseData = new GetProductArticlesByProductTypeResponse(
            productArticleDTOList,
            totalArticlesCount,
            request.PageNumber,
            request.PageSize);

        return new ApiQueryResponse<GetProductArticlesByProductTypeResponse>(true, (int)HttpStatusCode.OK)
        {
            Data = responseData
        };
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