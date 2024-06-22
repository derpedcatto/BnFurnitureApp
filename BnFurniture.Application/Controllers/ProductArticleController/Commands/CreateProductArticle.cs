using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductArticleController.DTO.Request;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductArticleController.Commands;

public sealed record CreateProductArticleCommand(CreateProductArticleDTO dto);

public sealed class CreateProductArticleHandler : CommandHandler<CreateProductArticleCommand>
{
    private readonly CreateProductArticleDTOValidator _validator;

    public CreateProductArticleHandler(CreateProductArticleDTOValidator validator,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(CreateProductArticleCommand command, CancellationToken cancellationToken)
    {
        var dto = command.dto;

        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Validation failed.",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        var newProductArticle = new ProductArticle()
        {
            Article = Guid.NewGuid(),
            ProductId = dto.ProductId,
            AuthorId = dto.AuthorId,
            Name = dto.Name,
            CreatedAt = DateTime.UtcNow,
            Price = dto.Price,
            Discount = dto.Discount,
            Active = dto.Active
        };

        await HandlerContext.DbContext.AddAsync(newProductArticle, cancellationToken);
        await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
        {
            Message = "Product article created."
        };
    }
}
