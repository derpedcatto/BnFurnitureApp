using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductArticleController.DTO.Request;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BnFurniture.Application.Controllers.ProductArticleController.Commands;

public sealed record UpdateProductArticleCommand(UpdateProductArticleDTO dto);

public sealed class UpdateProductArticleHandler : CommandHandler<UpdateProductArticleCommand>
{
    private readonly UpdateProductArticleDTOValidator _validator;

    public UpdateProductArticleHandler(UpdateProductArticleDTOValidator validator,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(UpdateProductArticleCommand command, CancellationToken cancellationToken)
    {
        var dto = command.dto;
        var dbContext = HandlerContext.DbContext;

        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Validation failed",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        var article = await dbContext.ProductArticle
            .FirstOrDefaultAsync(pa => pa.Article == dto.Article, cancellationToken);

        if (article == null)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.NotFound)
            {
                Message = "Article not found"
            };
        }

        article.ProductId = dto.ProductId;
        article.AuthorId = dto.AuthorId;
        article.Name = dto.Name;
        article.Price = dto.Price;
        article.Discount = dto.Discount;
        article.Active = dto.Active;
        article.UpdatedAt = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
        {
            Message = "Product Article updated successfully."
        };
    }
}
