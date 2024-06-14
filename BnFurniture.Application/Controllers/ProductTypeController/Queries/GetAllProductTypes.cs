using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductTypeController.DTO.Response;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductTypeController.Queries;

public sealed record GetAllProductTypesQuery(
    bool IncludeImages = true,
    bool RandomOrder = false,
    int? PageSize = null,
    int? PageNumber = null);

public sealed class GetAllProductTypesResponse
{
    public List<ProductTypeDTO> ProductTypes { get; set; }

    public GetAllProductTypesResponse(List<ProductTypeDTO> productTypes)
    {
        ProductTypes = productTypes;
    }
}

public sealed class GetAllProductTypesHandler : QueryHandler<GetAllProductTypesQuery, GetAllProductTypesResponse>
{
    public GetAllProductTypesHandler(
        IHandlerContext context) : base(context)
    {

    }

    public async override Task<ApiQueryResponse<GetAllProductTypesResponse>> Handle(
        GetAllProductTypesQuery query, CancellationToken cancellationToken)
    {
        IQueryable<ProductTypeDTO> productTypesQuery = 
            HandlerContext.DbContext.ProductType
                .Select(pt => new ProductTypeDTO
                {
                    Id = pt.Id,
                    CategoryId = pt.CategoryId,
                    Name = pt.Name,
                    Slug = pt.Slug,
                    Priority = pt.Priority
                });

        if (query.RandomOrder)
        {
            productTypesQuery = productTypesQuery.OrderBy(pt => Guid.NewGuid());
        }
        else
        {
            productTypesQuery = productTypesQuery.OrderBy(pt => pt.Name);
        }

        if (query.PageSize.HasValue && query.PageNumber.HasValue)
        {
            var skip = (query.PageNumber.Value - 1) * query.PageSize.Value;
            productTypesQuery = productTypesQuery.Skip(skip).Take(query.PageSize.Value);
        }

        var productTypes = await productTypesQuery.ToListAsync(cancellationToken);

        return new ApiQueryResponse<GetAllProductTypesResponse>(true, (int)HttpStatusCode.OK)
        {
            Data = new(productTypes)
        };
    }
}