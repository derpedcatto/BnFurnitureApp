using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.ProductTypeController.DTO.Request;

public class SetProductTypeImageDTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("cardImage")]
    public IFormFile CardImage { get; set; } = null!;

    [JsonPropertyName("thumbnailImage")]
    public IFormFile ThumbnailImage { get; set; } = null!;
}

public class SetProductTypeImageDTOValidator : AbstractValidator<SetProductTypeImageDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public SetProductTypeImageDTOValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Id).Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Id cannot be null.")
            .NotEmpty().WithMessage("Id cannot be empty.")
            .MustAsync(IsIdValid).WithMessage("Category with this Id does not exist.");

        RuleFor(x => x.CardImage)
            .NotNull().WithMessage("Image cannot be null.")
            .NotEmpty().WithMessage("Image cannot be empty.");

        RuleFor(x => x.ThumbnailImage)
            .NotNull().WithMessage("Image cannot be null.")
            .NotEmpty().WithMessage("Image cannot be empty.");
    }

    private async Task<bool> IsIdValid(Guid Id, CancellationToken ct)
    {
        return await _dbContext.ProductType.AnyAsync(pc => pc.Id == Id, ct);
    }
}