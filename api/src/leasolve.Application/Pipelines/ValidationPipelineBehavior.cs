using FluentValidation;
using leasolve.Application.Abstractions.CQRS;
using leasolve.Domain.Abstractions;
using MediatR;

namespace leasolve.Application.Pipelines;

internal sealed class ValidationPipelineBehavior<TRequest, TResponseValue>
    : IPipelineBehavior<TRequest, Result<TResponseValue>>
    where TRequest : ICommand<Result<TResponseValue>>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<Result<TResponseValue>> Handle(
        TRequest request,
        RequestHandlerDelegate<Result<TResponseValue>> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next(cancellationToken);

        var context = new ValidationContext<TRequest>(request);

        var failures = (await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))))
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (failures.Count == 0)
            return await next(cancellationToken);

        var errors = failures
            .GroupBy(f => f.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(f => f.ErrorMessage).Distinct().ToArray());
        
        // NOTE: if needed in future - return also errorCodes
        var validationError = new ValidationError(
            "Validation.Failed",
            "One or more validation errors occurred.",
            errors);

        return validationError;
    }
}