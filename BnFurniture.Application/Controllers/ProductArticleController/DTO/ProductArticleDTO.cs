using BnFurniture.Application.Extensions;
using FluentValidation;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.ProductArticleController.DTO;

public class ProductArticleDTO
{
    [JsonPropertyName("article")]
    public Guid Article { get; set; }

    [JsonPropertyName("productId")]
    public Guid ProductId { get; set; }

    [JsonPropertyName("authorId")]
    public Guid AuthorId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("discount")]
    public int Discount { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }
}

public class ProductArticleDTOValidator : AbstractValidator<ProductArticleDTO>
{
    public ProductArticleDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product article name cannot be empty.")
            .MaximumLength(255).WithMessage("Product article name must be less than 255 characters.");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug cannot be empty.")
            .UrlSlug();

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.Discount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount must be a positive integer or zero.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId cannot be empty.");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("AuthorId cannot be empty.");
    }
}
