using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.CategoryController.DTO.Request;

public class SetCategoryImageDTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("image")]
    public IFormFile Image { get; set; } = null!;
}

public class SetCategoryImageDTOValidator : AbstractValidator<SetCategoryImageDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public SetCategoryImageDTOValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Id).Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Id cannot be null.")
            .NotEmpty().WithMessage("Id cannot be empty.")
            .MustAsync(IsIdValid).WithMessage("Category with this Id does not exist.");

        RuleFor(x => x.Image)
            .NotNull().WithMessage("Image cannot be null.")
            .NotEmpty().WithMessage("Image cannot be empty.");
    }

    private async Task<bool> IsIdValid(Guid Id, CancellationToken ct)
    {
        return await _dbContext.ProductCategory.AnyAsync(pc => pc.Id == Id, ct);
    }
}