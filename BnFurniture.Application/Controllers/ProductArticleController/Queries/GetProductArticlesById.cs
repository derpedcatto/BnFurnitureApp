using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductArticleController.DTO.Request;
using BnFurniture.Application.Controllers.ProductArticleController.DTO.Response;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductArticleController.Queries;

public sealed record GetProductArticlesByIdsQuery(
    GetProductArticlesByIdDTO Dto);

public sealed class GetProductArticlesByIdsResponse
{
    public List<ProductArticleDTO> ProductArticles { get; set; }

    public GetProductArticlesByIdsResponse(List<ProductArticleDTO> productArticles)
    {
        ProductArticles = productArticles;
    }
}

public sealed class GetProductArticlesByIdsHandler : QueryHandler<GetProductArticlesByIdsQuery, GetProductArticlesByIdsResponse>
{
    private readonly IAppImageService _appImageService;

    public GetProductArticlesByIdsHandler(
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _appImageService = appImageService;
    }

    public override async Task<ApiQueryResponse<GetProductArticlesByIdsResponse>> Handle(
        GetProductArticlesByIdsQuery request, CancellationToken cancellationToken)
    {
        // Query including Product and ProductType
        var articles = await HandlerContext.DbContext.ProductArticle
            .Include(x => x.Product)
            .ThenInclude(p => p.ProductType)
            .Where(x => request.Dto.ArticleList.Contains(x.Article))
            .ToListAsync(cancellationToken);

        var dtos = new List<ProductArticleDTO>();
        foreach (var article in articles)
        {
            var dto = MapProductArticleToDTO(article);

            var imageResult = await _appImageService.GetImagesAsync(
                Domain.Enums.AppEntityType.Product,
                dto.ProductId,
                Domain.Enums.AppEntityImageType.Thumbnail,
                cancellationToken);

            dto.ThumbnailImageUri = imageResult.Data?.FirstOrDefault() ?? string.Empty;

            dtos.Add(dto);
        }

        return new ApiQueryResponse<GetProductArticlesByIdsResponse>
            (true, (int)HttpStatusCode.OK)
        {
            Data = new(dtos),
        };
    }

    private ProductArticleDTO MapProductArticleToDTO(ProductArticle productArticle)
    {
        return new ProductArticleDTO
        {
            Article = productArticle.Article,
            ProductId = productArticle.ProductId,
            AuthorId = productArticle.AuthorId,
            Name = productArticle.Name,
            CreatedAt = productArticle.CreatedAt,
            UpdatedAt = productArticle.UpdatedAt,
            Price = productArticle.Price,
            Discount = productArticle.Discount,
            Active = productArticle.Active,
            ProductTypeName = productArticle.Product.ProductType.Name // Map the ProductTypeName
        };
    }
}