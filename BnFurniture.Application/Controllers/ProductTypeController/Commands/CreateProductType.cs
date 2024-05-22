using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductTypeController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductTypeController.Commands;

public sealed record CreateProductTypeCommand(CreateProductTypeDTO dto);

public sealed class CreateProductTypeHandler : CommandHandler<CreateProductTypeCommand>
{
    private readonly CreateProductTypeDTOValidator _validator;

    public CreateProductTypeHandler(CreateProductTypeDTOValidator validator,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(CreateProductTypeCommand command, CancellationToken cancellationToken)
    {
        var dto = command.dto;
        var dbContext = HandlerContext.DbContext;

        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        var newProductType = new ProductType()
        {
            Id = Guid.NewGuid(),
            CategoryId = dto.CategoryId,
            Name = dto.Name,
            Slug = dto.Slug,
            Priority = dto.Priority,
        };
        
        await dbContext.ProductType.AddAsync(newProductType, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
        {
            Message = "Product Type created."
        };
    }
}