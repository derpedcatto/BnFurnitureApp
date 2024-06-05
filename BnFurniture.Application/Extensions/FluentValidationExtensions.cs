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

    public static IRuleBuilderOptions<T, string> UrlSlug<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        /*
        Should be english, alphanumeric phrases separated with a single dash or underscore.
        Should not contain single alpha character between dashes.
        Should not be comprised of just numbers and dashes.
        Should not begin with a number or a dash.
        Should not end with a dash.
         */

        return ruleBuilder
            .NotEmpty().WithMessage("URL slug must not be empty.")
            .Matches("^(?![0-9-_]+$)(?![_-])[a-z0-9]+(?:[_-][a-z0-9]+)*(?<![_-])$").WithMessage("Invalid URL slug.");
    }
}
