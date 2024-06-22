using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductTypeController.DTO.Request;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductTypeController.Commands;

public sealed record UpdateProductTypeCommand(UpdateProductTypeDTO Dto);

public sealed class UpdateProductTypeHandler : CommandHandler<UpdateProductTypeCommand>
{
    private readonly UpdateProductTypeDTOValidator _validator;

    public UpdateProductTypeHandler(UpdateProductTypeDTOValidator validator,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(
        UpdateProductTypeCommand request, CancellationToken cancellationToken)
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

        var productType = await HandlerContext.DbContext.ProductType
            .FirstOrDefaultAsync(x => x.Id == request.Dto.Id, cancellationToken);

        productType!.CategoryId = request.Dto.CategoryId;
        productType.Name = request.Dto.Name;
        productType.Slug = request.Dto.Slug;
        productType.Priority = request.Dto.Priority;

        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse
            (true, (int)HttpStatusCode.OK)
        {
            Message = "Product Type updated successfully."
        };
    }
}