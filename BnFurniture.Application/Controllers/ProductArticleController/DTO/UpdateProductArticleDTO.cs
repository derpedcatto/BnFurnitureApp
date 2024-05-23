using BnFurniture.Application.Extensions;
using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.ProductArticleController.DTO;

public class UpdateProductArticleDTO
{
    [JsonPropertyName("article")]
    public Guid Article { get; set; }

    [JsonPropertyName("productId")]
    public Guid ProductId { get; set; }

    [JsonPropertyName("authorId")]
    public Guid AuthorId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("discount")]
    public int Discount { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }
}

public class UpdateProductArticleDTOValidator : AbstractValidator<UpdateProductArticleDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateProductArticleDTOValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Article)
            .NotEmpty().WithMessage("Article ID cannot be empty.")
            .MustAsync(DoesArticleExist).WithMessage("Article with the specified ID does not exist.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product ID cannot be empty.")
            .MustAsync(IsProductIdValid).WithMessage("Product with the specified ID does not exist.");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("Author ID cannot be empty.")
            .MustAsync(IsAuthorIdValid).WithMessage("Author with the specified ID does not exist.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be a positive value.");

        RuleFor(x => x.Discount)
            .InclusiveBetween(0, 100).WithMessage("Discount must be between 0 and 100.");
    }

    private async Task<bool> DoesArticleExist(Guid articleId, CancellationToken ct)
    {
        return await _dbContext.ProductArticle.AnyAsync(a => a.Article == articleId, ct);
    }

    private async Task<bool> IsProductIdValid(Guid productId, CancellationToken ct)
    {
        return await _dbContext.Product.AnyAsync(p => p.Id == productId, ct);
    }

    private async Task<bool> IsAuthorIdValid(Guid authorId, CancellationToken ct)
    {
        return await _dbContext.User.AnyAsync(u => u.Id == authorId, ct);
    }
}
