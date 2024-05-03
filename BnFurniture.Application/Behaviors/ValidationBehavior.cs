using FluentValidation;
using Mediator;

namespace BnFurniture.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async ValueTask<TResponse> Handle(
        TRequest request, 
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TRequest, TResponse> next)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(x => x.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(x => x != null)
            .Distinct()
            .ToList();

        if (failures.Count != 0)
        {
            throw new ValidationException(failures);
        }

        return await next(request, cancellationToken);
    }
}

/*
public class ValidationResult<TRequest, TResponse>
{
    public bool IsValid { get; set; }
    public IEnumerable<ValidationFailure> Failures { get; set; }
    public TResponse Data { get; set; }
}
*/




/*
if (!_validators.Any())
{
    return await next(request, cancellationToken);
}

var errors = new List<ValidationFailure>();
var context = new ValidationContext<TRequest>(request);

foreach ( var validator in _validators )
{
    var validationResult = await validator.ValidateAsync(context);
    if (validationResult.Errors != null && validationResult.Errors.Count > 0) 
    {
        errors.AddRange(validationResult.Errors);
    }
}

if (errors.Count != 0)
{
    throw new ValidationException(errors);
}

return await next (request, cancellationToken);
*/





// .Select(failure => new ErrorModel (failure.PropertyName, failure.ErrorMessage))

/*
return new ValidationResult<TRequest, TResponse>
{
    IsValid = false,
    Failures = failures,
    Data = default(TResponse)
};
*/

/*
return new ValidationResult<TRequest, TResponse>
{
    IsValid = true,
    Failures = Array.Empty<ValidationFailure>(),
    Data = result
};
*/




/* MediatR
    public async ValueTask<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(x => x.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(x => x != null)
            .Distinct()
            .ToList();

        if (failures.Count != 0)
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
*/