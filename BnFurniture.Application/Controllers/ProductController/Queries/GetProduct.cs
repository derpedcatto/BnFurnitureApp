using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductController.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductController.Queries;

public sealed record GetProductQuery(Guid productId);

public sealed class GetProductResponse
{
    public ResponseProductDTO Product { get; set; }

    public GetProductResponse(ResponseProductDTO product)
    {
        Product = product;
    }
}

public sealed class GetProductHandler : QueryHandler<GetProductQuery, GetProductResponse>
{
    public GetProductHandler(
        IHandlerContext context) : base(context)
    {
        
    }

    public override async Task<ApiQueryResponse<GetProductResponse>> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var product = await HandlerContext.DbContext.Product
            .Where(p => p.Id == query.productId)
            .Select(p => new ResponseProductDTO
            {
                Id = p.Id,
                ProductTypeId = p.ProductTypeId,
                AuthorId = p.AuthorId,
                Name = p.Name,
                Slug = p.Slug,
                Summary = p.Summary,
                Description = p.Description,
                ProductDetails = p.ProductDetails,
                Priority = p.Priority,
                Active = p.Active,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).FirstAsync(cancellationToken);

        return new ApiQueryResponse<GetProductResponse>(true, (int)HttpStatusCode.OK)
        {
            Message = "Product fetch success",
            Data = new(product)
        };
    }
}
