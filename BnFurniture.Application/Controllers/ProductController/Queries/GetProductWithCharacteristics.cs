using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CharacteristicController.DTO.Response;
using BnFurniture.Application.Controllers.CharacteristicValueController.DTO.Response;
using BnFurniture.Application.Controllers.ProductController.DTO.Response;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace BnFurniture.Application.Controllers.ProductController.Queries
{
    public sealed record GetProductWithCharacteristicsQuery(string Slug);

    public sealed class GetProductWithCharacteristicsResponse
    {
        public ProductWithCharacteristicsDTO Product { get; set; }

        public GetProductWithCharacteristicsResponse(ProductWithCharacteristicsDTO product)
        {
            Product = product;
        }
    }

    public sealed class GetProductWithCharacteristicsHandler : QueryHandler<GetProductWithCharacteristicsQuery, GetProductWithCharacteristicsResponse>
    {
        public GetProductWithCharacteristicsHandler(
            IHandlerContext context) : base(context)
        { 
        
        }

        public override async Task<ApiQueryResponse<GetProductWithCharacteristicsResponse>> Handle(GetProductWithCharacteristicsQuery request, CancellationToken cancellationToken)
        {
            var product = await HandlerContext.DbContext.Product
                .Include(p => p.ProductType)
                .Include(p => p.Author)
                .Include(p => p.Metrics)
                .Include(p => p.ProductSetItems)
                .Include(p => p.ProductReviews)
                .Include(p => p.ProductArticles)
                .ThenInclude(pa => pa.ProductCharacteristicConfigurations)
                .ThenInclude(pcc => pcc.Characteristic)
                .ThenInclude(c => c.CharacteristicValues)
                .Where(p => p.Slug == request.Slug)
                .FirstOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                return new ApiQueryResponse<GetProductWithCharacteristicsResponse>(false, 404)
                {
                    Message = "Product not found."
                };
            }

            var characteristicDtos = product.ProductArticles
                .SelectMany(pa => pa.ProductCharacteristicConfigurations)
                .GroupBy(pcc => pcc.Characteristic)
                .Select(g => new CharacteristicWithValuesDTO
                {
                    Id = g.Key.Id,
                    Name = g.Key.Name,
                    Slug = g.Key.Slug,
                    Priority = g.Key.Priority,
                    Values = g.Select(pcc => new CharacteristicValueDTO
                    {
                        Id = pcc.CharacteristicValue.Id,
                        CharacteristicId = pcc.CharacteristicValue.CharacteristicId,
                        Value = pcc.CharacteristicValue.Value,
                        Slug = pcc.CharacteristicValue.Slug,
                        Priority = pcc.CharacteristicValue.Priority
                    })
                    .DistinctBy(cv => cv.Id)
                    .OrderBy(s => s.Slug)
                    .ToList()
                }).ToList();

            var response = new ProductWithCharacteristicsDTO
            {
                Id = product.Id,
                ProductTypeId = product.ProductTypeId,
                AuthorId = product.AuthorId,
                Name = product.Name,
                Slug = product.Slug,
                Summary = product.Summary,
                Description = product.Description,
                ProductDetails = product.ProductDetails,
                Priority = product.Priority,
                Active = product.Active,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                Characteristics = characteristicDtos
            };

            return new ApiQueryResponse<GetProductWithCharacteristicsResponse>(true, 200) { Data = new(response) };
        }
    }
}
