using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductTypeController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductTypeController.Commands;

public sealed record UpdateProductTypeCommand(UpdateProductTypeDTO dto);

public sealed class UpdateProductTypeHandler : CommandHandler<UpdateProductTypeCommand>
{
    private readonly UpdateProductTypeDTOValidator _validator;

    public UpdateProductTypeHandler(UpdateProductTypeDTOValidator validator,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(UpdateProductTypeCommand command, CancellationToken cancellationToken)
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

        var productType = await dbContext.ProductType
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        productType!.CategoryId = dto.CategoryId;
        productType.Name = dto.Name;
        productType.Slug = dto.Slug;
        productType.Priority = dto.Priority;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
        {
            Message = "Product Type updated successfully."
        };
    }
}