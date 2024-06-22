using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductArticleController.DTO.Response;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductArticleController.Queries;

public sealed record GetAllProductArticlesQuery(
    int PageNumber,
    int PageSize);

public sealed class GetAllProductArticlesResponse
{
    public List<ProductArticleDTO> Articles { get; private set; }
    public int TotalCount { get; private set; }
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }

    public GetAllProductArticlesResponse(List<ProductArticleDTO> productArticles)
    {
        Articles = productArticles;
    }

    public GetAllProductArticlesResponse(List<ProductArticleDTO> productArticles, int totalCount, int pageNumber, int pageSize)
    {
        Articles = productArticles;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

public sealed class GetAllProductArticlesHandler : QueryHandler<GetAllProductArticlesQuery, GetAllProductArticlesResponse>
{
    private readonly IAppImageService _appImageService;

    public GetAllProductArticlesHandler(
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _appImageService = appImageService;
    }

    public override async Task<ApiQueryResponse<GetAllProductArticlesResponse>> Handle(
        GetAllProductArticlesQuery request, CancellationToken cancellationToken)
    {
        var dbContext = HandlerContext.DbContext;

        var totalArticlesCount = await dbContext.ProductArticle.CountAsync(cancellationToken);

        var productArticles = await dbContext.ProductArticle
            .Include(pa => pa.Product)
                .ThenInclude(pa => pa.ProductType)
            .Include(pa => pa.Author)
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

        var responseData = new GetAllProductArticlesResponse(
            productArticleDTOList,
            totalArticlesCount,
            request.PageNumber,
            request.PageSize);

        return new ApiQueryResponse<GetAllProductArticlesResponse>(true, (int)HttpStatusCode.OK)
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