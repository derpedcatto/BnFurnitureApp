using BnFurniture.Application.Abstractions;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.CategoryController.Commands;

public sealed record DeleteCategoryCommand(Guid CategoryId);

public sealed class DeleteCategoryHandler : CommandHandler<DeleteCategoryCommand>
{
    public DeleteCategoryHandler(
        IHandlerContext context) : base(context)
    {

    }

    public override async Task<ApiCommandResponse> Handle(
        DeleteCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = await HandlerContext.DbContext.ProductCategory
            .Where(c => c.Id == request.CategoryId)
            .SingleOrDefaultAsync(cancellationToken);

        if (category == null)
        {
            return new ApiCommandResponse
                (false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку",
                Errors = new() { ["categoryId"] = ["Category ID not found in database."] }
            };
        }

        HandlerContext.DbContext.ProductCategory.Remove(category);
        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse
            (true, (int)HttpStatusCode.OK)
        {
            Message = "Category deleted successfully."
        };
    }
}