using BnFurniture.Application.Extensions;
using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.ProductController.DTO.Request;

public class CreateProductWithArticlesDTO
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

    [JsonPropertyName("price")]
    public int Price { get; set; }

    [JsonPropertyName("discount")]
    public int Discount { get; set; }

    [JsonPropertyName("characteristics")]
    public List<CharacteristicInputDTO> Characteristics { get; set; } = [];
}

public class CharacteristicInputDTO
{
    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("valueSlugs")]
    public List<string> ValueSlugs { get; set; } = [];
}

public class CreateProductWithArticlesDTOValidator : AbstractValidator<CreateProductWithArticlesDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateProductWithArticlesDTOValidator(ApplicationDbContext dbContext)
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

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.Discount)
            .InclusiveBetween(0, 99).WithMessage("Discount must be between 0 and 99.");

        RuleForEach(x => x.Characteristics)
            .SetValidator(new CharacteristicInputDTOValidator(_dbContext));
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
        return !await _dbContext.Product.AnyAsync(c => c.Slug == slug, ct);
    }
}

public class CharacteristicInputDTOValidator : AbstractValidator<CharacteristicInputDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public CharacteristicInputDTOValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Characteristic slug cannot be empty.")
            .MustAsync(IsCharacteristicSlugValid).WithMessage("Characteristic slug does not exist.");

        RuleFor(x => x.ValueSlugs)
            .NotEmpty().WithMessage("Value slugs list cannot be empty.")
            .MustAsync((dto, valueSlugs, ct) => AreValueSlugsValid(dto.Slug, valueSlugs, ct))
            .WithMessage("One or more value slugs are invalid for the given characteristic.");
    }

    private async Task<bool> IsCharacteristicSlugValid(string slug, CancellationToken ct)
    {
        return await _dbContext.Characteristic.AnyAsync(c => c.Slug == slug, ct);
    }

    private async Task<bool> AreValueSlugsValid(string characteristicSlug, List<string> valueSlugs, CancellationToken ct)
    {
        var characteristic = await _dbContext.Characteristic
            .Include(c => c.CharacteristicValues)
            .FirstOrDefaultAsync(c => c.Slug == characteristicSlug, ct);

        if (characteristic == null)
        {
            return false;
        }

        return valueSlugs.All(vs => characteristic.CharacteristicValues.Any(cv => cv.Slug == vs));
    }
}