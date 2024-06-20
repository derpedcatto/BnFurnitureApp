using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductController.DTO.Request;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductController.Commands;

public sealed record CreateProductCommand(CreateProductDTO dto);

public sealed class CreateProductHandler : CommandHandler<CreateProductCommand>
{
    private readonly CreateProductDTOValidator _validator;

    public CreateProductHandler(CreateProductDTOValidator validator,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var dto = command.dto;

        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Validation fail",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        var newProduct = new Product
        {
            Id = Guid.NewGuid(),
            ProductTypeId = dto.ProductTypeId,
            AuthorId = dto.AuthorId,
            Name = dto.Name,
            Slug = dto.Slug,
            Summary = dto.Summary,
            Description = dto.Description,
            ProductDetails = dto.ProductDetails,
            Priority = dto.Priority,
            Active = dto.Active,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };

        var newProductMetrics = new ProductMetrics
        {
            Id = Guid.NewGuid(),
            ProductId = newProduct.Id,
        };

        HandlerContext.DbContext.Product.Add(newProduct);
        HandlerContext.DbContext.ProductMetrics.Add(newProductMetrics);
        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
        {
            Message = "Product created successfully."
        };
    }
}