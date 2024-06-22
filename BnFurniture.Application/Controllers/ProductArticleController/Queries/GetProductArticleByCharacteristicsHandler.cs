using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductArticleController.DTO.Response;
using BnFurniture.Application.Services.AppImageService;
using BnFurniture.Domain.Enums;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace BnFurniture.Application.Controllers.ProductArticleController.Queries;

public sealed record GetProductArticleByCharacteristicsQuery(
    string Slug,
    bool IncludeImages);

public sealed class GetProductArticleByCharacteristicsResponse
{
    public ProductArticleDTO Article { get; set; }

    public GetProductArticleByCharacteristicsResponse(ProductArticleDTO article)
    {
        Article = article;
    }
}

public sealed class GetProductArticleByCharacteristicsHandler : QueryHandler<GetProductArticleByCharacteristicsQuery, GetProductArticleByCharacteristicsResponse>
{
    private readonly IAppImageService _appImageService;

    public GetProductArticleByCharacteristicsHandler(
        IAppImageService appImageService,
        IHandlerContext context) : base(context)
    {
        _appImageService = appImageService;
    }

    public override async Task<ApiQueryResponse<GetProductArticleByCharacteristicsResponse>> Handle(
        GetProductArticleByCharacteristicsQuery request, CancellationToken cancellationToken)
    {
        // 1 - Get slug and Divide slug to two parts - product | characteristic values
        var slugs = request.Slug.Split(new[] { '-' }, 2);
        if (slugs.Length < 2)
        {
            return new ApiQueryResponse<GetProductArticleByCharacteristicsResponse>(false, 400) { Message = "Invalid slug format." };
        }

        var slugProduct = slugs[0];
        var slugsCharacteristicValues = slugs[1].Split('-').ToList();

        // Get product by slug and include characteristic configurations
        var product = await HandlerContext.DbContext.Product
            .Include(p => p.ProductType)
            .Include(p => p.ProductArticles)
                .ThenInclude(pa => pa.ProductCharacteristicConfigurations)
                    .ThenInclude(pcc => pcc.CharacteristicValue)
            .Include(p => p.ProductArticles)
                .ThenInclude(pa => pa.ProductCharacteristicConfigurations)
                    .ThenInclude(pcc => pcc.Characteristic)
            .FirstOrDefaultAsync(p => p.Slug == slugProduct, cancellationToken);

        if (product == null)
        {
            return new ApiQueryResponse<GetProductArticleByCharacteristicsResponse>
                (false, 404)
            {
                Message = "Product not found."
            };
        }

        // List-pair that includes Article and its characteristic value slugs
        var articlesWithConfigurations = product.ProductArticles
            .Select(pa => new
            {
                Article = pa,
                Configurations = pa.ProductCharacteristicConfigurations
                    .OrderBy(pcc => pcc.Characteristic.Slug) // Order by characteristic slug
                    .Select(pcc => pcc.CharacteristicValue.Slug).ToList()
            }).ToList();

        // Find the article that matches the ordered list of characteristic value slugs
        var matchingArticle = articlesWithConfigurations
            .FirstOrDefault(a => a.Configurations.SequenceEqual(slugsCharacteristicValues))?
            .Article;

        if (matchingArticle == null)
        {
            return new ApiQueryResponse<GetProductArticleByCharacteristicsResponse>
                (false, 404) 
            {
                Message = "Product Article with specified characteristics not found"
            };
        }

        var response = new ProductArticleDTO
        {
            Article = matchingArticle.Article,
            ProductId = matchingArticle.ProductId,
            AuthorId = matchingArticle.AuthorId,
            Name = matchingArticle.Name,
            CreatedAt = matchingArticle.CreatedAt,
            UpdatedAt = matchingArticle.UpdatedAt,
            Price = matchingArticle.Price,
            Discount = matchingArticle.Discount,
            Active = matchingArticle.Active,
            ProductTypeName = product.ProductType.Name
        };

        if (request.IncludeImages)
        {
            var imageGalleryResult = await _appImageService.GetImagesAsync(
                AppEntityType.ProductArticle,
                response.Article,
                AppEntityImageType.Gallery,
                cancellationToken);

            var imageThumbResult = await _appImageService.GetImagesAsync(
                AppEntityType.Product,
                response.ProductId,
                AppEntityImageType.Thumbnail,
                cancellationToken);

            response.GalleryImages = imageGalleryResult.Data!.ToList();
            response.ThumbnailImageUri = imageThumbResult.Data.FirstOrDefault();
        }

        return new ApiQueryResponse<GetProductArticleByCharacteristicsResponse>
            (true, 200) 
        { 
            Data = new(response) 
        };
    }
}

/* v1
        // 1 - Get slug and Divide slug to two parts - product | characteristic values
        var slugs = request.Slug.Split(new[] { '-' }, 2);
        if (slugs.Length < 2)
        {
            return new ApiQueryResponse<GetProductArticleByCharacteristicsResponse>(false, 400) { Message = "Invalid slug format." };
        }

        var slugProduct = slugs[0];
        var slugsCharacteristicValues = slugs[1].Split('-').ToList();

        // 2.1 - Get product by first slug
        var product = await HandlerContext.DbContext.Product
            .Include(p => p.ProductArticles)
            .FirstOrDefaultAsync(p => p.Slug == slugProduct, cancellationToken);

        if (product == null)
        {
            return new ApiQueryResponse<GetProductArticleByCharacteristicsResponse>
                (false, 404) 
            { 
                Message = "Product not found."
            };
        }

        // 2.2 - Getting all product characteristics and Sorting alphabetically by slugs
        var productCharacteristics = await HandlerContext.DbContext.ProductArticle
            .Where(pa => pa.ProductId == product.Id)
            .SelectMany(pa => pa.ProductCharacteristicConfigurations)
            .Select(pcc => pcc.Characteristic)
            .Distinct()
            .OrderBy(a => a.Slug)
            .ToListAsync(cancellationToken);

        // 3. Match each characteristic value to a characteristic
        var characteristicValueMap = new Dictionary<Guid, Guid>(); // CharacteristicId - CharacteristicValueId
        foreach (var characteristic in productCharacteristics)
        {
            var matchingValue = await HandlerContext.DbContext.CharacteristicValue
                .FirstOrDefaultAsync(cv => 
                    cv.CharacteristicId == characteristic.Id && 
                    slugsCharacteristicValues.Contains(cv.Slug),
                cancellationToken);

            if (matchingValue != null)
            {
                characteristicValueMap[characteristic.Id] = matchingValue.Id;
            }
        }

        if (characteristicValueMap.Count != productCharacteristics.Count)
        {
            return new ApiQueryResponse<GetProductArticleByCharacteristicsResponse>
                (false, 400) 
            {
                Message = "Not all characteristic values matched."
            };
        }

        // 4. Get product article based on matched characteristic values
        var productArticle = await HandlerContext.DbContext.ProductArticle
            .Where(pa => pa.ProductId == product.Id)
            .FirstOrDefaultAsync(pa => pa.ProductCharacteristicConfigurations
                .All(pcc => characteristicValueMap.ContainsKey(pcc.CharacteristicId) &&
                            pcc.CharacteristicValueId == characteristicValueMap[pcc.CharacteristicId]), cancellationToken);

        if (productArticle == null)
        {
            return new ApiQueryResponse<GetProductArticleByCharacteristicsResponse>(false, 404) { Message = "Product Article with specified characteristics not found" };
        }

        var response = new ProductArticleDTO
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

        return new ApiQueryResponse<GetProductArticleByCharacteristicsResponse>(true, 200) { Data = new(response) };
*/

/* Original implementation
var characteristicValues = await HandlerContext.DbContext.CharacteristicValue
    .Where(cv => slugsCharacteristicValues.Contains(cv.Slug))
    .OrderBy(cv => cv.Slug)
    .ToListAsync(cancellationToken);

if (characteristicValues.Count != slugsCharacteristicValues.Count)
{
    return new ApiQueryResponse<GetProductArticleByCharacteristicsResponse>(false, 404) { Message = "One or more characteristic values not found." };
}

var matchingArticle = await HandlerContext.DbContext.ProductCharacteristicConfiguration
    .Where(pcc => pcc.ProductArticle.ProductId == product.Id &&
                    characteristicValues
                        .Select(cv => cv.Id)
                        .Contains(pcc.CharacteristicValueId))
    .Select(pcc => pcc.ProductArticle)
    .FirstOrDefaultAsync(cancellationToken);

if (matchingArticle == null)
{
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
*/