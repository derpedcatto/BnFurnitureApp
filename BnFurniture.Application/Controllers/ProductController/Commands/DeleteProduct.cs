using BnFurniture.Application.Abstractions;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductController.Commands;

public sealed record DeleteProductCommand(Guid productId);

public sealed class DeleteProductHandler : CommandHandler<DeleteProductCommand>
{
    public DeleteProductHandler(
        IHandlerContext context) : base(context)
    {
    }

    public override async Task<ApiCommandResponse> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var dbContext = HandlerContext.DbContext;

        var product = await dbContext.Product
            .Where(c => c.Id == command.productId)
            .Include(a => a.ProductType)
            .Include(b => b.Author)
            .FirstOrDefaultAsync(cancellationToken);

        if (product == null)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.NotFound)
            {
                Message = "Валідація не пройшла перевірку",
                Errors = new() { ["productId"] = ["Product ID not found in database."] }
            };
        }

        dbContext.Product.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
        {
            Message = "Product deleted successfully."
        };
    }
}