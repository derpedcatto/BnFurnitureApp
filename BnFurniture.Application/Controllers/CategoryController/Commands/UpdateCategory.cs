using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.DTO.Request;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.CategoryController.Commands;

public sealed record UpdateCategoryCommand(UpdateCategoryDTO Dto);

public sealed class UpdateCategoryHandler : CommandHandler<UpdateCategoryCommand>
{
    private readonly UpdateCategoryDTOValidator _validator;

    public UpdateCategoryHandler(
        UpdateCategoryDTOValidator validator,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse
                (false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        var category = await HandlerContext.DbContext.ProductCategory
            .Where(a => a.Id == request.Dto.Id)
            .FirstOrDefaultAsync(cancellationToken);

        category!.ParentId = request.Dto.ParentId;
        category.Name = request.Dto.Name;
        category.Slug = request.Dto.Slug;
        category.Priority = request.Dto.Priority;

        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse
            (true, (int)HttpStatusCode.OK)
        {
            Message = "Category Update Success."
        };
    }
}
