using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductController.DTO.Response;
using BnFurniture.Domain.Responses;

namespace BnFurniture.Application.Controllers.ProductController.Queries;

public sealed record GetProductBySlugQuery(
    string ProductSlug);

public sealed class GetProductBySlugResponse
{
    public ProductWithCharacteristicsDTO Product { get; set; }

    public GetProductBySlugResponse(ProductWithCharacteristicsDTO product)
    {
        Product = product;
    }
}

public sealed class GetProductBySlugHandler : QueryHandler<GetProductBySlugQuery, GetProductBySlugResponse>
{
    public GetProductBySlugHandler(
        IHandlerContext context) : base(context)
    {
    }

    public override async Task<ApiQueryResponse<GetProductBySlugResponse>> Handle(
        GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        var productHandler = new GetProductWithCharacteristicsHandler(HandlerContext);
        var result = await productHandler.Handle(new(request.ProductSlug), cancellationToken);

        var responseData = result.Data != null
            ? new GetProductBySlugResponse(result.Data.Product)
            : null;

        return new ApiQueryResponse<GetProductBySlugResponse>
            (result.IsSuccess, result.StatusCode)
        {
            Message = result.Message,
            Data = responseData,
        };
    }
}
