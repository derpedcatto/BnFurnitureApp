using BnFurniture.Application.Extensions;
using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.CategoryController.DTO;

public class CreateCategoryDTO
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("priority")]
    public int? Priority { get; set; }

    [JsonPropertyName("parentId")]
    public Guid? ParentId { get; set; }

    [JsonPropertyName("promoCardThumbnailImage")]
    public IFormFile? PromoCardThumbnailImage { get; set; }
}

public class CreateCategoryDTOValidator : AbstractValidator<CreateCategoryDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateCategoryDTOValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is null.")
            .NotEmpty().WithMessage("Name is empty.");

        RuleFor(x => x.Slug).Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Slug is null.")
            .NotEmpty().WithMessage("Slug is empty.")
            .UrlSlug()
            .MustAsync(IsSlugUnique).WithMessage("Slug is not unique.");

        RuleFor(x => x.ParentId)
            .MustAsync((dto, id, ct) => { return IsParentIdValid(dto.ParentId!.Value, ct); }).WithMessage("Parent Category with this Id does not exist")
                .When(x => x.ParentId != null);
    }

    private async Task<bool> IsSlugUnique(string slug, CancellationToken ct)
    {
        return ! await _dbContext.ProductCategory.AnyAsync(c => c.Slug == slug, ct);
    }

    private async Task<bool> IsParentIdValid(Guid parentId, CancellationToken ct)
    {
        return await _dbContext.ProductCategory.AnyAsync(pc => pc.Id == parentId, ct);
    }
}