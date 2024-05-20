using FluentValidation;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.ExampleController.DTO;

public class ExampleEntityFormDTO
{
    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("temperatureC")]
    public int TemperatureC { get; set; }

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }
}

public class ExampleEntityFormDTOValidator : AbstractValidator<ExampleEntityFormDTO>
{
    public ExampleEntityFormDTOValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Дата не може бути порожньою.")
            .Must(IsValidDate).WithMessage("Дата повинна бути в форматі DateOnly.");

        RuleFor(x => x.TemperatureC)
            .NotEmpty().WithMessage("Температура не може бути порожньою.")
            .LessThanOrEqualTo(250).WithMessage("Температура не може бути вище 250 градусів.")
            .GreaterThanOrEqualTo(-250).WithMessage("Температура не може бути нижчою за -250 градусів.");

        When(x => x.Summary != null, () =>
        {
            RuleFor(x => x.Summary)
                .Must(p => p.Any(char.IsUpper)).WithMessage("Поле повинно мати хоча б одну велику літеру.")
                .Must(p => p.Any(char.IsLower)).WithMessage("Поле повинно мати хоча б одну малу літеру.")
                .Must(p => p.Any(char.IsNumber)).WithMessage("Поле повинно мати хоча б одну цифру.");
        });
    }

    private bool IsValidDate(DateOnly date)
    {
        return date >= DateOnly.MinValue;
    }
}