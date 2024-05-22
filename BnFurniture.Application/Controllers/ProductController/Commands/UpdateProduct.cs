using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductController.Commands;

public sealed record UpdateProductCommand(UpdateProductDTO dto);

public sealed class UpdateProductHandler : CommandHandler<UpdateProductCommand>
{
    private readonly UpdateProductDTOValidator _validator;

    public UpdateProductHandler(UpdateProductDTOValidator validator,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
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

        var product = await dbContext.Product
            .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        product!.ProductTypeId = dto.ProductTypeId;
        product.AuthorId = dto.AuthorId;
        product.Name = dto.Name;
        product.Slug = dto.Slug;
        product.Summary = dto.Summary;
        product.Description = dto.Description;
        product.ProductDetails = dto.ProductDetails;
        product.Priority = dto.Priority;
        product.Active = dto.Active;
        product.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
        {
            Message = "Product updated successfully."
        };
    }
}