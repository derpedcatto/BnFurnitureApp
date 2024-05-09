using FluentValidation;

namespace BnFurniture.Application.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
           .Length(12).WithMessage("Номер телефону має містити 12 цифр.")
           .Must(number => number.StartsWith("380")).WithMessage("Номер телефону має починатися з коду '380'.");
    }
}
