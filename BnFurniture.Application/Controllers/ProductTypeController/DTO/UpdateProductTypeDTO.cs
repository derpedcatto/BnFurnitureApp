using BnFurniture.Application.Extensions;
using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.ProductTypeController.DTO;

public class UpdateProductTypeDTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("categoryId")]
    public Guid CategoryId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("priority")]
    public int? Priority { get; set; }
}

public class UpdateProductTypeDTOValidator : AbstractValidator<UpdateProductTypeDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateProductTypeDTOValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Id).Cascade(CascadeMode.Stop)
           .NotNull().WithMessage("Category Id cannot be null.")
           .NotEmpty().WithMessage("Category Id cannot be empty.")
           .MustAsync(IsProductTypeIdValid).WithMessage("Invalid ProductType Id.");

        RuleFor(x => x.CategoryId).Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Category Id cannot be null.")
            .NotEmpty().WithMessage("Category Id cannot be empty.")
            .MustAsync(IsCategoryIdValid).WithMessage("Parent Category with this Id does not exist");

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is null.")
            .NotEmpty().WithMessage("Name is empty.");

        RuleFor(x => x.Slug).Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Slug is null.")
            .NotEmpty().WithMessage("Slug is empty.")
            .UrlSlug()
            .MustAsync((dto, slug, ct) => { return IsCategorySlugUnique(dto.CategoryId, dto.Slug, ct); }).WithMessage("Linked category already contains the exact slug string.");

        RuleFor(x => x.Priority)
            .GreaterThanOrEqualTo(0).WithMessage("Priority must be a positive integer or zero.")
                .When(x => x.Priority.HasValue);
    }
    private async Task<bool> IsCategorySlugUnique(Guid categoryId, string slug, CancellationToken ct)
    {
        return await _dbContext.ProductCategory
            .AnyAsync(pc => pc.Id == categoryId && pc.Slug == slug, ct);
    }

    private async Task<bool> IsProductTypeIdValid(Guid typeId, CancellationToken ct)
    {
        return await _dbContext.ProductType.AnyAsync(pc => pc.Id == typeId, ct);
    }

    private async Task<bool> IsCategoryIdValid(Guid categoryId, CancellationToken ct)
    {
        return await _dbContext.ProductCategory.AnyAsync(pc => pc.Id == categoryId, ct);
    }
}