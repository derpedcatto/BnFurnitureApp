using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CharacteristicValueController.DTO;
using BnFurniture.Application.Controllers.ProductCharacteristicController.DTO;
using BnFurniture.Application.Controllers.ProductController.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace BnFurniture.Application.Controllers.ProductController.Queries
{
    public sealed record GetFilteredProductsQuery(ProductsFilteredSearchRequestDTO Request);

    public sealed class GetFilteredProductsHandler : QueryHandler<GetFilteredProductsQuery, ResponseWithPaginationDTO<ResponseProductQueryDTO>>
{
    public GetFilteredProductsHandler(IHandlerContext context) : base(context) { }

    public override async Task<ApiQueryResponse<ResponseWithPaginationDTO<ResponseProductQueryDTO>>> Handle(GetFilteredProductsQuery request, CancellationToken cancellationToken)

    {
        var dto = request.Request;

        // Создание запроса для продукта с учетом фильтрации и пагинации
        var query = HandlerContext.DbContext.Product
            .Include(p => p.ProductArticles)
                .ThenInclude(pa => pa.ProductCharacteristicConfigurations)
                    .ThenInclude(pcc => pcc.Characteristic)
                        .ThenInclude(c => c.CharacteristicValues)
            .AsQueryable();

        // Фильтрация по тексту
        if (!string.IsNullOrWhiteSpace(dto.Text))
        {
            query = query.Where(p => p.Name.Contains(dto.Text, StringComparison.OrdinalIgnoreCase));
        }

        // Фильтрация по диапазону цен
        if (!string.IsNullOrWhiteSpace(dto.PriceRange))
        {
            var priceParts = dto.PriceRange.Split('-');
            if (priceParts.Length == 2 && decimal.TryParse(priceParts[0], out decimal minPrice) && decimal.TryParse(priceParts[1], out decimal maxPrice))
            {
                query = query.Where(p => p.ProductArticles
                    .Any(pa => pa.Price >= minPrice && pa.Price <= maxPrice));
            }
        }

        // Фильтрация по характеристикам
        foreach (var characteristic in dto.Characteristics)
        {
            var characteristicSlug = characteristic.Key;
            var characteristicValues = characteristic.Value;

            query = query.Where(p => p.ProductArticles
                .Any(pa => pa.ProductCharacteristicConfigurations
                    .Any(pcc => pcc.Characteristic.Slug == characteristicSlug &&
                    pcc.Characteristic.CharacteristicValues
                        .Any(cv => characteristicValues.Contains(cv.Slug)))));
        }

            // Пагинация
            var totalCount = await query.CountAsync(cancellationToken);
            var products = await query
                .Skip((dto.Page - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .Select(p => new ResponseProductQueryDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.ProductArticles
                    .Select(pa => pa.Price)
                    .FirstOrDefault(),
                            Characteristics = p.ProductArticles
                    .SelectMany(pa => pa.ProductCharacteristicConfigurations)
                    .Select(pc => new ResponseCharacteristicWithValuesDTO
            {
                        Id = pc.Characteristic.Id,
                        Name = pc.Characteristic.Name,
                        Slug = pc.Characteristic.Slug,
                        Priority = pc.Characteristic.Priority,
                        Values = HandlerContext.DbContext.CharacteristicValue
                       .Where(v => v.CharacteristicId == pc.Characteristic.Id)
                       .Select(v => new CharacteristicValueDTO // Используйте правильный тип здесь
                       {
                            Id = v.Id,
                            CharacteristicId = v.CharacteristicId,
                            Value = v.Value,
                            Slug = v.Slug,
                            Priority = v.Priority
                        })
                            .ToList()
                    })
                    .ToList()
            })
            .ToListAsync(cancellationToken);

        // Формирование ответа
        var response = new ResponseWithPaginationDTO<ResponseProductQueryDTO>
        {
            Data = products,
            Pagination = new PaginationInfo
            {
                Page = dto.Page,
                PageSize = dto.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)dto.PageSize)
            }
        };

        return new ApiQueryResponse<ResponseWithPaginationDTO<ResponseProductQueryDTO>>(true, (int)HttpStatusCode.OK)

        {
            Data = response
        };
    }
}


}
