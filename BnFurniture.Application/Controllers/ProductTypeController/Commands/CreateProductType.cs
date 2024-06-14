using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductTypeController.DTO.Request;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductTypeController.Commands;

public sealed record CreateProductTypeCommand(CreateProductTypeDTO Dto);

public sealed class CreateProductTypeHandler : CommandHandler<CreateProductTypeCommand>
{
    private readonly CreateProductTypeDTOValidator _validator;

    public CreateProductTypeHandler(
        CreateProductTypeDTOValidator validator,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(
        CreateProductTypeCommand request, CancellationToken cancellationToken)
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

        var newProductType = new ProductType()
        {
            Id = Guid.NewGuid(),
            CategoryId = request.Dto.CategoryId,
            Name = request.Dto.Name,
            Slug = request.Dto.Slug,
            Priority = request.Dto.Priority,
        };
        
        await HandlerContext.DbContext.ProductType.AddAsync(newProductType, cancellationToken);
        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse
            (true, (int)HttpStatusCode.Created)
        {
            Message = "Product Type created."
        };
    }
}