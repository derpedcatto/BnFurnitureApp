using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.CategoryController.Commands;

public sealed record UpdateCategoryCommand(UpdateCategoryDTO dto);

public sealed class UpdateCategoryHandler : CommandHandler<UpdateCategoryCommand>
{
    private readonly UpdateCategoryDTOValidator _validator;

    public UpdateCategoryHandler(UpdateCategoryDTOValidator validator,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var dbContext = HandlerContext.DbContext;
        var dto = command.dto;

        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        var category = await dbContext.ProductCategory
            .Where(a => a.Id == dto.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        category!.ParentId = dto.ParentId;
        category.Name = dto.Name;
        category.Slug = dto.Slug;
        category.Priority = dto.Priority;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
        {
            Message = "Category Update Success."
        };
    }
}
