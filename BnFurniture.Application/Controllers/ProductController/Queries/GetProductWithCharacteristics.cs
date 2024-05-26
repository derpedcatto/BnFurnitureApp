﻿using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductCharacteristicController.DTO;
using BnFurniture.Application.Controllers.ProductController.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductController.Queries
{
    public sealed record GetProductWithCharacteristicsQuery(string Slug);

    public sealed class GetProductWithCharacteristicsHandler : QueryHandler<GetProductWithCharacteristicsQuery, ResponseProductDTO>
    {
        public GetProductWithCharacteristicsHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiQueryResponse<ResponseProductDTO>> Handle(GetProductWithCharacteristicsQuery request, CancellationToken cancellationToken)
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
                return new ApiQueryResponse<ResponseProductDTO>(false, 404)
                {
                    Message = "Product not found."
                };
            }

            var characteristicDtos = product.ProductArticles
                .SelectMany(pa => pa.ProductCharacteristicConfigurations)
                .GroupBy(pcc => pcc.Characteristic)
                .Select(g => new ResponseCharacteristicWithValuesDTO
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
                    }).ToList()
                }).ToList();

            var response = new ResponseProductDTO
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

            return new ApiQueryResponse<ResponseProductDTO>(true, 200) { Data = response };
        }
    }
}
