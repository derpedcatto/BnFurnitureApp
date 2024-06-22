using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductArticleController.DTO.Response;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductArticleController.Queries;

public sealed record GetProductArticleSlugByIdQuery(
    Guid ArticleId);

public sealed class GetProductArticleSlugByIdResponse
{
    public ProductArticleSlugDTO ArticleSlug {  get; set; }

    public GetProductArticleSlugByIdResponse(ProductArticleSlugDTO articleSlug)
    {
        ArticleSlug = articleSlug;
    }
}

public sealed class GetProductArticleSlugByIdHandler : QueryHandler<GetProductArticleSlugByIdQuery, GetProductArticleSlugByIdResponse>
{
    public GetProductArticleSlugByIdHandler(
        IHandlerContext context) : base(context)
    {

    }

    public override async Task<ApiQueryResponse<GetProductArticleSlugByIdResponse>> Handle(
            GetProductArticleSlugByIdQuery request, CancellationToken cancellationToken)
    {
        var article = await HandlerContext.DbContext.ProductArticle
            .Include(pa => pa.Product)
            .Include(pa => pa.ProductCharacteristicConfigurations)
                .ThenInclude(pcc => pcc.Characteristic)
            .Include(pa => pa.ProductCharacteristicConfigurations)
                .ThenInclude(pcc => pcc.CharacteristicValue)
            .FirstOrDefaultAsync(x => x.Article == request.ArticleId, cancellationToken);

        if (article == null)
        {
            return new ApiQueryResponse<GetProductArticleSlugByIdResponse>(
                false, (int)HttpStatusCode.NotFound)
            {
                Message = "Article not found",
                Data = null,
            };
        }

        // Sorting the characteristic values based on characteristic slug alphabetically
        var sortedCharacteristics = article.ProductCharacteristicConfigurations
            .OrderBy(pcc => pcc.Characteristic.Slug)
            .Select(pcc => pcc.CharacteristicValue.Slug)
            .ToList();

        // Constructing the slug from product slug and sorted characteristic values
        string productSlug = article.Product.Slug;
        string characteristicsSlug = string.Join("-", sortedCharacteristics);
        string finalSlug = $"{productSlug}-{characteristicsSlug}";

        ProductArticleSlugDTO dto = new()
        {
            Slug = finalSlug,
        };

        return new ApiQueryResponse<GetProductArticleSlugByIdResponse>(
            true, (int)HttpStatusCode.OK)
        {
            Message = "Success",
            Data = new(dto),
        };
    }
}