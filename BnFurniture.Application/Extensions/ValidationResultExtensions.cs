using FluentValidation.Results;

namespace BnFurniture.Application.Extensions;

public static class ValidationResultExtensions
{
    public static Dictionary<string, List<string>>? ToApiResponseErrors(this ValidationResult validationResult)
    {
        if (validationResult == null || validationResult.Errors.Count == 0)
        {
            return null;
        }

        return validationResult.Errors.GroupBy(e => e.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(e => e.ErrorMessage).ToList()
            );
    }
}