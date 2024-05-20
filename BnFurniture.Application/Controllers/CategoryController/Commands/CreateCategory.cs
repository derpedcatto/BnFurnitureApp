using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System.Net;

namespace BnFurniture.Application.Controllers.CategoryController.Commands;

public sealed record CreateCategoryCommand(CreateCategoryDTO initialForm);

public sealed class CreateCategoryHandler : CommandHandler<CreateCategoryCommand>
{
    private readonly CreateCategoryDTOValidator _validator;

    public CreateCategoryHandler(CreateCategoryDTOValidator validator,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var dto = request.initialForm;

        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        var newCategory = new ProductCategory()
        {
            Id = Guid.NewGuid(),
            ParentId = dto.ParentId == null ? null : dto.ParentId,
            Name = dto.Name,
            Slug = dto.Slug,
            Priority = dto.Priority
        };

        await HandlerContext.DbContext.AddAsync(newCategory, cancellationToken);
        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
        {
            Message = "Category created."
        };
    }
}