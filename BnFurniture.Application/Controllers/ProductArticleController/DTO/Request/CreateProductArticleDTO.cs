using BnFurniture.Application.Extensions;
using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.ProductArticleController.DTO.Request;

public class CreateProductArticleDTO
{
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

public class CreateProductArticleDTOValidator : AbstractValidator<CreateProductArticleDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateProductArticleDTOValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is null.")
            .NotEmpty().WithMessage("Name is empty.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.Discount)
            .InclusiveBetween(0, 99).WithMessage("Discount must be between 0 and 99.");

        RuleFor(x => x.ProductId)
            .MustAsync(IsProductIdValid).WithMessage("Product with this Id does not exist.");

        RuleFor(x => x.AuthorId)
            .MustAsync(IsAuthorIdValid).WithMessage("Author with this Id does not exist.");
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
