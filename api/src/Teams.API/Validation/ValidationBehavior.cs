using FluentValidation;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace Teams.API.Validation;

/// <remarks>
/// Synchronous implementation adapted from eShop reference application:
/// https://github.com/dotnet/eShop/blob/main/src/Ordering.API/Application/Behaviors/ValidatorBehavior.cs
/// </remarks>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

    public ValidationBehavior(
        IEnumerable<IValidator<TRequest>> validators,
        ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating {Request}", typeof(TRequest).Name);
        
        var failures = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();
            
        if (failures.Any())
        {
            _logger.LogWarning("Validation errors - {Request} - Errors: {@ValidationErrors}",
                typeof(TRequest).Name, failures);
            
            throw new ValidationException(failures);
        }

        return await next(cancellationToken);
    }
}