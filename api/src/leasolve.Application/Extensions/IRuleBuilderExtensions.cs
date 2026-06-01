using FluentValidation;
using FluentValidation.Results;
using leasolve.Domain.Abstractions;

namespace leasolve.Application.Extensions;

public static class IRuleBuilderExtensions
{
    public static IRuleBuilder<T, TProperty> MustBeValidValueObject<T, TValue, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder,
        Func<TProperty, Result<TValue>> factory)
    {
        ArgumentNullException.ThrowIfNull(factory);

        return ruleBuilder.Custom((value, context) =>
        {
            Result<TValue> result = factory(value);

            if (!result.IsFailure)
                return;
            
            context.AddFailure(new ValidationFailure(context.PropertyPath, result.Error.Details)
            {
                ErrorCode = result.Error.Code
            });
        });
    }
}