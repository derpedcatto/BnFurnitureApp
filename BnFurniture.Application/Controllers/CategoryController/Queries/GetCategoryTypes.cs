using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.Shared;
using BnFurniture.Application.Controllers.ProductTypeController.DTO.Response;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.CategoryController.Queries;

public sealed record GetCategoryTypesQuery(
    Guid CategoryId,
    bool IncludeImages);

public sealed class GetCategoryTypesResponse
{
    public List<ProductTypeDTO>? ProductTypes;

    public GetCategoryTypesResponse(List<ProductTypeDTO>? productTypes)
    {
        ProductTypes = productTypes;
    }
}

public sealed class GetCategoryTypesHandler : QueryHandler<GetCategoryTypesQuery, GetCategoryTypesResponse>
{
    private readonly CategoryControllerSharedLogic _sharedLogic;

    public GetCategoryTypesHandler(
        CategoryControllerSharedLogic sharedService,
        IHandlerContext context) : base(context)
    {
        _sharedLogic = sharedService;
    }

    public override async Task<ApiQueryResponse<GetCategoryTypesResponse>> Handle(
        GetCategoryTypesQuery request,
        CancellationToken cancellationToken)
    {
        var category = await HandlerContext.DbContext.ProductCategory
            .Where(i => i.Id == request.CategoryId)
            .FirstOrDefaultAsync(cancellationToken);

        if (category == null)
        {
            return new ApiQueryResponse<GetCategoryTypesResponse>
                (false, (int)HttpStatusCode.NotFound)
            {
                Message = "Category with this ID is not found",
                Data = null
            };
        }

        var categoryTypes = await _sharedLogic.GetProductTypesForCategory(
            request.CategoryId, request.IncludeImages, cancellationToken);

        return new ApiQueryResponse<GetCategoryTypesResponse>
            (true, (int)HttpStatusCode.OK)
        {
            Data = new(categoryTypes),
        };
    }
}