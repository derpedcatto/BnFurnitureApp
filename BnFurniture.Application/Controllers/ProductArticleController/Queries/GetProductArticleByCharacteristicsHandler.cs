using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductArticleController.DTO;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductArticleController.Queries
{
    public sealed record GetProductArticleByCharacteristicsQuery(string Slug);
    public sealed class GetProductArticleByCharacteristicsHandler : QueryHandler<GetProductArticleByCharacteristicsQuery, ProductArticleResponseDTO>
    {
        private readonly ILogger<GetProductArticleByCharacteristicsHandler> _logger;

        public GetProductArticleByCharacteristicsHandler(IHandlerContext context, ILogger<GetProductArticleByCharacteristicsHandler> logger) : base(context)
        {
            _logger = logger;
        }

        public override async Task<ApiQueryResponse<ProductArticleResponseDTO>> Handle(GetProductArticleByCharacteristicsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling request with slug: {Slug}", request.Slug);

            var slugs = request.Slug.Split(new[] { '-' }, 2); // Разделяем строку на 2 части
            if (slugs.Length < 2)
            {
                _logger.LogError("Invalid slug format: {Slug}", request.Slug);
                return new ApiQueryResponse<ProductArticleResponseDTO>(false, 400) { Message = "Invalid slug format." };
            }

            var productSlug = slugs[0];
            var characteristicValueSlugs = slugs[1].Split('-').ToList();

            _logger.LogInformation("Fetching product with slug: {ProductSlug}", productSlug);
            var product = await HandlerContext.DbContext.Product
                .Include(p => p.ProductArticles)
                .FirstOrDefaultAsync(p => p.Slug == productSlug, cancellationToken);

            if (product == null)
            {
                _logger.LogWarning("Product not found: {ProductSlug}", productSlug);
                return new ApiQueryResponse<ProductArticleResponseDTO>(false, 404) { Message = "Product not found." };
            }

            _logger.LogInformation("Fetching characteristic values with slugs: {CharacteristicValueSlugs}", string.Join(", ", characteristicValueSlugs));
            var characteristicValues = await HandlerContext.DbContext.CharacteristicValue
                .Where(cv => characteristicValueSlugs.Contains(cv.Slug))
                .ToListAsync(cancellationToken);

            if (characteristicValues.Count != characteristicValueSlugs.Count)
            {
                _logger.LogWarning("One or more characteristic values not found: {CharacteristicValueSlugs}", string.Join(", ", characteristicValueSlugs));
                return new ApiQueryResponse<ProductArticleResponseDTO>(false, 404) { Message = "One or more characteristic values not found." };
            }

            _logger.LogInformation("Characteristic values found: {CharacteristicValues}", string.Join(", ", characteristicValues.Select(cv => cv.Slug)));

            _logger.LogInformation("Fetching matching product article");
            var matchingArticle = await HandlerContext.DbContext.ProductCharacteristicConfiguration
                .Where(pcc => pcc.ProductArticle.ProductId == product.Id &&
                              characteristicValues.Select(cv => cv.Id).Contains(pcc.CharacteristicValueId))
                .Select(pcc => pcc.ProductArticle)
                .FirstOrDefaultAsync(cancellationToken);

            if (matchingArticle == null)
            {
                _logger.LogWarning("No matching product article found for product: {ProductSlug}", productSlug);
                return new ApiQueryResponse<ProductArticleResponseDTO>(false, 404) { Message = "No matching product article found." };
            }

            var response = new ProductArticleResponseDTO
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

            return new ApiQueryResponse<ProductArticleResponseDTO>(true, 200) { Data = response };
        }
    }
}
