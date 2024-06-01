using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductTypeController.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductTypeController.Queries;

public sealed record GetAllProductTypesQuery();

public sealed class GetAllProductTypesResponse
{
    public List<ResponseProductTypeDTO> ProductTypes { get; set; }

    public GetAllProductTypesResponse(List<ResponseProductTypeDTO> productTypes)
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

    public async override Task<ApiQueryResponse<GetAllProductTypesResponse>> Handle(GetAllProductTypesQuery query, CancellationToken cancellationToken)
    {
        var productTypes = await HandlerContext.DbContext.ProductType
            .Select(pt => new ResponseProductTypeDTO
            {
                Id = pt.Id,
                CategoryId = pt.CategoryId,
                Name = pt.Name,
                Slug = pt.Slug,
                Priority = pt.Priority
            })
            .OrderBy(pt => pt.Name)
            .ToListAsync(cancellationToken);

        return new ApiQueryResponse<GetAllProductTypesResponse>(true, (int)HttpStatusCode.OK)
        {
            Data = new(productTypes)
        };
    }
}