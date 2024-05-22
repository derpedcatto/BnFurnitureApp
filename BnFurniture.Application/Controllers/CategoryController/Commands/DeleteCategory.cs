using BnFurniture.Application.Abstractions;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.CategoryController.Commands;

public sealed record DeleteCategoryCommand(Guid categoryId);

public sealed class DeleteCategoryHandler : CommandHandler<DeleteCategoryCommand>
{
    public DeleteCategoryHandler(
        IHandlerContext context) : base(context)
    {

    }

    public override async Task<ApiCommandResponse> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var dbContext = HandlerContext.DbContext;
        var categoryId = command.categoryId;

        var category = await dbContext.ProductCategory
            .Where(c => c.Id == categoryId)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

        if (category == null)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку",
                Errors = new() { ["categoryId"] = ["Category ID not found in database"] }
            };
        }

        dbContext.ProductCategory.Remove(category);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
        {
            Message = "Category deleted successfully."
        };
    }
}