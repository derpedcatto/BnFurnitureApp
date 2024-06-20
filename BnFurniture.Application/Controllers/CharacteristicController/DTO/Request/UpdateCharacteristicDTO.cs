using BnFurniture.Application.Controllers.CharacteristicValueController.DTO.Request;
using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.CharacteristicController.DTO.Request
{
    public class UpdateCharacteristicDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonPropertyName("priority")]
        public int? Priority { get; set; }
    }

    public class UpdateCharacteristicDTOValidator : AbstractValidator<UpdateCharacteristicDTO>
    {
        private readonly ApplicationDbContext _dbContext;

        public UpdateCharacteristicDTOValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Id).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Id cannot be null.")
                .NotEmpty().WithMessage("Id cannot be empty.")
                .MustAsync(IsIdValid).WithMessage("Characteristic with this Id does not exist.");

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

        private async Task<bool> IsSlugUnique(UpdateCharacteristicDTO dto, string slug, CancellationToken ct)
        {
            return !await _dbContext.Characteristic.AnyAsync(c => c.Slug == slug && c.Id != dto.Id, ct);
        }

        private async Task<bool> IsIdValid(Guid id, CancellationToken ct)
        {
            return await _dbContext.Characteristic.AnyAsync(c => c.Id == id, ct);
        }
    }
    public class UpdateCharacteristicValueDTOValidator : AbstractValidator<UpdateCharacteristicValueDTO>
    {
        private readonly ApplicationDbContext _dbContext;

        public UpdateCharacteristicValueDTOValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Id).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Id cannot be null.")
                .NotEmpty().WithMessage("Id cannot be empty.")
                .MustAsync(IsIdValid).WithMessage("Characteristic value with this Id does not exist.");

            RuleFor(x => x.Value)
                .NotNull().WithMessage("Value is null.")
                .NotEmpty().WithMessage("Value is empty.");

            RuleFor(x => x.Slug).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Slug is null.")
                .NotEmpty().WithMessage("Slug is empty.")
                .MustAsync(IsSlugUnique).WithMessage("Slug is not unique.");

            RuleFor(x => x.Priority)
                .GreaterThanOrEqualTo(0).WithMessage("Priority must be a positive integer or zero.")
                .When(x => x.Priority.HasValue);
        }

        private async Task<bool> IsSlugUnique(string slug, CancellationToken ct)
        {
            return !await _dbContext.CharacteristicValue.AnyAsync(cv => cv.Slug == slug, ct);
        }

        private async Task<bool> IsIdValid(Guid id, CancellationToken ct)
        {
            return await _dbContext.CharacteristicValue.AnyAsync(cv => cv.Id == id, ct);
        }
    }
}
