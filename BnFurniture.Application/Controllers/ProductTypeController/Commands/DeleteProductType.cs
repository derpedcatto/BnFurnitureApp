using BnFurniture.Application.Abstractions;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductTypeController.Commands;

public sealed record DeleteProductTypeCommand(Guid ProductTypeId);

public sealed class DeleteProductTypeHandler : CommandHandler<DeleteProductTypeCommand>
{
    public DeleteProductTypeHandler(
        IHandlerContext context) : base(context)
    {

    }

    public override async Task<ApiCommandResponse> Handle(
        DeleteProductTypeCommand request, CancellationToken cancellationToken)
    {
        var productType = await HandlerContext.DbContext.ProductType
            .Include(pt => pt.Products)
            .Where(c => c.Id == request.ProductTypeId)
            .SingleOrDefaultAsync(cancellationToken);

        if (productType == null)
        {
            return new ApiCommandResponse
                (false, (int)HttpStatusCode.NotFound)
            {
                Message = "Валідація не пройшла перевірку",
                Errors = new() { ["productTypeId"] = ["Product Type ID not found in database."] }
            };
        }

        if (productType.Products.Count != 0)
        {
            return new ApiCommandResponse
                (false, (int)HttpStatusCode.Conflict)
            {
                Message = "Product Type cannot be deleted because it has associated products.",
                Errors = new() { ["productTypeId"] = ["Cannot delete Product Type with associated Products."] }
            };
        }

        HandlerContext.DbContext.ProductType.Remove(productType);
        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse
            (true, (int)HttpStatusCode.OK)
        {
            Message = "Product Type deleted successfully."
        };
    }
}