using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductArticleController.DTO.Response;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductArticleController.Queries;

public sealed record GetProductArticleByIdQuery(
    Guid ArticleId);

public sealed class GetProductArticleByIdResponse
{
    public ProductArticleDTO ProductArticle { get; set; }

    public GetProductArticleByIdResponse(ProductArticleDTO productArticle)
    {
        ProductArticle = productArticle;
    }
}

public sealed class GetProductArticleByIdHandler : QueryHandler<GetProductArticleByIdQuery, GetProductArticleByIdResponse>
{
    private readonly IAppImageService _appImageService;

    public GetProductArticleByIdHandler(
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _appImageService = appImageService;   
    }

    public override async Task<ApiQueryResponse<GetProductArticleByIdResponse>> Handle(
        GetProductArticleByIdQuery request, CancellationToken cancellationToken)
    {
        var article = await HandlerContext.DbContext.ProductArticle
            .Where(x => x.Article == request.ArticleId)
            .FirstOrDefaultAsync(cancellationToken);

        var dto = MapProductArticleToDTO(article!);

        var imageResult = await _appImageService.GetImagesAsync(
            Domain.Enums.AppEntityType.Product,
            dto.Article,
            Domain.Enums.AppEntityImageType.Thumbnail,
            cancellationToken);

        dto.ThumbnailImageUri = imageResult.Data?.FirstOrDefault() ?? string.Empty;

        return new ApiQueryResponse<GetProductArticleByIdResponse>
            (true, (int)HttpStatusCode.OK)
        {
            Message = "",
            Data = new(dto),
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
        };
    }

}
