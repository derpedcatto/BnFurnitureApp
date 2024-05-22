using BnFurniture.Application.Extensions;
using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.CategoryController.DTO;

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

        RuleFor(x => x.Id)
            .MustAsync(IsIdValid).WithMessage("Category with this Id does not exist.");

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is null.")
            .NotEmpty().WithMessage("Name is empty.");

        RuleFor(x => x.Slug)
            .NotNull().WithMessage("Slug is null.")
            .NotEmpty().WithMessage("Slug is empty.")
            .UrlSlug();

        When(x => x.ParentId != null, () =>
        {
            RuleFor(x => x.ParentId)
                .MustAsync((dto, parentId, ct) => { return IsParentIdValid(dto.ParentId, ct); })
                    .WithMessage("Parent Category with this Id does not exist.");
        });
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