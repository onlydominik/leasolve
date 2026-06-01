using FluentValidation;
using leasolve.Application.Extensions;
using leasolve.Domain.ValueObjects;

namespace leasolve.Application.Features.Users.Users.Commands.CreateTenant;

public sealed class CreateTenantValidator : AbstractValidator<CreateTenantCommand>
{
    public CreateTenantValidator()
    {
        RuleFor(x => x.Email).MustBeValidValueObject(Email.Create);
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
    }
}