using BnFurniture.Application.Extensions;
using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.ProductController.DTO;

public class CreateProductDTO
{
    [JsonPropertyName("productTypeId")]
    public Guid ProductTypeId { get; set; }

    [JsonPropertyName("authorId")]
    public Guid AuthorId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("productDetails")]
    public string? ProductDetails { get; set; }

    [JsonPropertyName("priority")]
    public int? Priority { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }
}

public class CreateProductDTOValidator : AbstractValidator<CreateProductDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateProductDTOValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.ProductTypeId)
            .MustAsync(IsProductTypeIdValid).WithMessage("Invalid Product Type ID.");

        RuleFor(x => x.AuthorId)
            .MustAsync(IsAuthorIdValid).WithMessage("Invalid Author ID.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name cannot be empty.");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug cannot be empty.")
            .UrlSlug()
            .MustAsync(IsSlugUnique).WithMessage("Slug is not unique.");

        RuleFor(x => x.Priority)
            .GreaterThanOrEqualTo(0).WithMessage("Priority must be a positive integer or zero.")
                .When(x => x.Priority.HasValue);
    }

    private async Task<bool> IsProductTypeIdValid(Guid id, CancellationToken ct)
    {
        return await _dbContext.ProductType.AnyAsync(a => a.Id == id, ct);
    }

    private async Task<bool> IsAuthorIdValid(Guid id, CancellationToken ct)
    {
        return await _dbContext.User.AnyAsync(a => a.Id == id, ct);
    }

    private async Task<bool> IsSlugUnique(string slug, CancellationToken ct)
    {
        return ! await _dbContext.Product.AnyAsync(c => c.Slug == slug, ct);
    }
}