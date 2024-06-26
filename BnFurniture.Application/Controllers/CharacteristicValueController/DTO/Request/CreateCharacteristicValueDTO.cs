﻿using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.CharacteristicValueController.DTO.Request;

public class CreateCharacteristicValueDTO
{
    [JsonPropertyName("characteristicId")]
    public Guid CharacteristicId { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("priority")]
    public int? Priority { get; set; }
}

public class CreateCharacteristicValueDTOValidator : AbstractValidator<CreateCharacteristicValueDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateCharacteristicValueDTOValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Value)
            .NotNull().WithMessage("Value is null.")
            .NotEmpty().WithMessage("Value is empty.");

        RuleFor(x => x.Slug)
            .NotNull().WithMessage("Slug is null.")
            .NotEmpty().WithMessage("Slug is empty.")
            .MustAsync(IsSlugUnique).WithMessage("Slug is not unique.");

        RuleFor(x => x.Priority)
            .GreaterThanOrEqualTo(0).WithMessage("Priority must be a positive integer or zero.")
            .When(x => x.Priority.HasValue);

        RuleFor(x => x.CharacteristicId)
            .MustAsync(IsCharacteristicIdValid).WithMessage("Characteristic with this Id does not exist.");
    }

    private async Task<bool> IsSlugUnique(CreateCharacteristicValueDTO dto, string slug, CancellationToken ct)
    {
        return !await _dbContext.CharacteristicValue.AnyAsync(cv => cv.Slug == slug, ct);
    }

    private async Task<bool> IsCharacteristicIdValid(Guid characteristicId, CancellationToken ct)
    {
        return await _dbContext.Characteristic.AnyAsync(c => c.Id == characteristicId, ct);
    }
}
