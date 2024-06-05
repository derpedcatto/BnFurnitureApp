using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BnFurniture.Application.Controllers.CharacteristicController.DTO;

public class CreateCharacteristicDTO
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? Priority { get; set; }
}

public class CreateCharacteristicDTOValidator : AbstractValidator<CreateCharacteristicDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateCharacteristicDTOValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is null.")
            .NotEmpty().WithMessage("Name is empty.");

        RuleFor(x => x.Slug).Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Slug is null.")
            .NotEmpty().WithMessage("Slug is empty.")
            .MustAsync(IsSlugUnique).WithMessage("Slug is not unique.");

        RuleFor(x => x.Priority)
            .GreaterThanOrEqualTo(0).WithMessage("Priority must be a positive integer or zero.")
                .When(x => x.Priority.HasValue);
    }

    private async Task<bool> IsSlugUnique(CreateCharacteristicDTO dto, string slug, CancellationToken ct)
    {
        return !await _dbContext.Characteristic.AnyAsync(c => c.Slug == slug, ct);
    }
}
