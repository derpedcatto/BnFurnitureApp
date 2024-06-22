using BnFurniture.Application.Extensions;
using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.CategoryController.DTO.Request;

public class UpdateCategoryDTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("parentId")]
    public Guid? ParentId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("priority")]
    public int? Priority { get; set; }
}

public class UpdateCategoryDTOValidator : AbstractValidator<UpdateCategoryDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateCategoryDTOValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Id).Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Id cannot be null.")
            .NotEmpty().WithMessage("Id cannot be empty.")
            .MustAsync(IsIdValid).WithMessage("Category with this Id does not exist.");

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is null.")
            .NotEmpty().WithMessage("Name is empty.");

        RuleFor(x => x.Slug).Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Slug is null.")
            .NotEmpty().WithMessage("Slug is empty.")
            .UrlSlug()
            .MustAsync(IsSlugUnique).WithMessage("Slug is not unique.");

        When(x => x.ParentId != null, () =>
        {
            RuleFor(x => x.ParentId)
                .MustAsync((dto, parentId, ct) => { return IsParentIdValid(dto.ParentId, ct); })
                    .WithMessage("Parent Category with this Id does not exist.");
        });

        RuleFor(x => x.Priority)
            .GreaterThanOrEqualTo(0).WithMessage("Priority must be a positive integer or zero.")
                .When(x => x.Priority.HasValue);
    }

    private async Task<bool> IsSlugUnique(string slug, CancellationToken ct)
    {
        return await _dbContext.ProductCategory.AnyAsync(c => c.Slug == slug, ct);
    }

    private async Task<bool> IsIdValid(Guid Id, CancellationToken ct)
    {
        return await _dbContext.ProductCategory.AnyAsync(pc => pc.Id == Id, ct);
    }

    private async Task<bool> IsParentIdValid(Guid? parentId, CancellationToken ct)
    {
        if (parentId == null) { return false; }
        return await _dbContext.ProductCategory.AnyAsync(pc => pc.Id == parentId, ct);
    }
}