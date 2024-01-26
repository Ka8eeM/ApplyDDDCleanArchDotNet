using FluentValidation;
using MediatR;
using Project.Domain.Common.Errors;
using ValidationResult = Project.Domain.Common.Errors.ValidationResult;

namespace Project.Application.Common.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result

{

    private readonly IEnumerable<IValidator<TRequest>>? _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>>? validators = null)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Validate Request
        // If any errors, return validation result
        // Otherwise, return next()

        if (_validators is null || !_validators.Any())
        {
            return await next();
        }

        var errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new Error(failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
            // return validation result
            return createValidationResult<TResponse>(errors);
        }

        return await next();

    }

    private static TResult createValidationResult<TResult>(Error[] errors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WithErrors(errors) as TResult)!;
        }
        object validationResult = typeof(ValidationResult<>)
        .GetGenericTypeDefinition()
        .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
        .GetMethod(nameof(ValidationResult.WithErrors))!
        .Invoke(null, new object?[] { errors })!;
        return (TResult)validationResult;
    }
}