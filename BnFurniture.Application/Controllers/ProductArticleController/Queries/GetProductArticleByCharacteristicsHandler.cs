using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductArticleController.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BnFurniture.Application.Controllers.ProductArticleController.Queries;

public sealed record GetProductArticleByCharacteristicsQuery(string Slug);

public sealed class GetProductArticleByCharacteristicsResponse
{
    public ResponseProductArticleDTO Article { get; set; }

    public GetProductArticleByCharacteristicsResponse(ResponseProductArticleDTO article)
    {
        Article = article;
    }
}

public sealed class GetProductArticleByCharacteristicsHandler : QueryHandler<GetProductArticleByCharacteristicsQuery, GetProductArticleByCharacteristicsResponse>
{
    public GetProductArticleByCharacteristicsHandler(
        IHandlerContext context) : base(context)
    {

    }

    public override async Task<ApiQueryResponse<GetProductArticleByCharacteristicsResponse>> Handle(GetProductArticleByCharacteristicsQuery request, CancellationToken cancellationToken)
    {
        HandlerContext.Logger.LogInformation("Handling request with slug: {Slug}", request.Slug);

        var slugs = request.Slug.Split(['-'], 2); // Разделяем строку на 2 части
        if (slugs.Length < 2)
        {
            HandlerContext.Logger.LogError("Invalid slug format: {Slug}", request.Slug);
            return new ApiQueryResponse<GetProductArticleByCharacteristicsResponse>(false, 400) { Message = "Invalid slug format." };
        }

        var productSlug = slugs[0];
        var characteristicValueSlugs = slugs[1].Split('-').ToList();

        HandlerContext.Logger.LogInformation("Fetching product with slug: {ProductSlug}", productSlug);
        var product = await HandlerContext.DbContext.Product
            .Include(p => p.ProductArticles)
            .FirstOrDefaultAsync(p => p.Slug == productSlug, cancellationToken);

        if (product == null)
        {
            HandlerContext.Logger.LogWarning("Product not found: {ProductSlug}", productSlug);
            return new ApiQueryResponse<GetProductArticleByCharacteristicsResponse>(false, 404) { Message = "Product not found." };
        }

        HandlerContext.Logger.LogInformation("Fetching characteristic values with slugs: {CharacteristicValueSlugs}", string.Join(", ", characteristicValueSlugs));
        var characteristicValues = await HandlerContext.DbContext.CharacteristicValue
            .Where(cv => characteristicValueSlugs.Contains(cv.Slug))
            .ToListAsync(cancellationToken);

        if (characteristicValues.Count != characteristicValueSlugs.Count)
        {
            HandlerContext.Logger.LogWarning("One or more characteristic values not found: {CharacteristicValueSlugs}", string.Join(", ", characteristicValueSlugs));
            return new ApiQueryResponse<GetProductArticleByCharacteristicsResponse>(false, 404) { Message = "One or more characteristic values not found." };
        }

        HandlerContext.Logger.LogInformation("Characteristic values found: {CharacteristicValues}", string.Join(", ", characteristicValues.Select(cv => cv.Slug)));

        HandlerContext.Logger.LogInformation("Fetching matching product article");
        var matchingArticle = await HandlerContext.DbContext.ProductCharacteristicConfiguration
            .Where(pcc => pcc.ProductArticle.ProductId == product.Id &&
                          characteristicValues
                            .Select(cv => cv.Id)
                            .Contains(pcc.CharacteristicValueId))
            .Select(pcc => pcc.ProductArticle)
            .FirstOrDefaultAsync(cancellationToken);

        if (matchingArticle == null)
        {
            HandlerContext.Logger.LogWarning("No matching product article found for product: {ProductSlug}", productSlug);
            return new ApiQueryResponse<GetProductArticleByCharacteristicsResponse>(false, 404) { Message = "No matching product article found." };
        }

        var response = new ResponseProductArticleDTO
        {
            Article = matchingArticle.Article,
            ProductId = matchingArticle.ProductId,
            AuthorId = matchingArticle.AuthorId,
            Name = matchingArticle.Name,
            CreatedAt = matchingArticle.CreatedAt,
            UpdatedAt = matchingArticle.UpdatedAt,
            Price = matchingArticle.Price,
            Discount = matchingArticle.Discount,
            Active = matchingArticle.Active
        };

        return new ApiQueryResponse<GetProductArticleByCharacteristicsResponse>(true, 200) { Data = new(response) };
    }
}
