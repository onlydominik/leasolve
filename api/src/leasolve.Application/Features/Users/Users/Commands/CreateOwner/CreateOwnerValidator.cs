using FluentValidation;
using leasolve.Application.Extensions;
using leasolve.Domain.ValueObjects;

namespace leasolve.Application.Features.Users.Users.Commands.CreateOwner;

public sealed class CreateOwnerValidator : AbstractValidator<CreateOwnerCommand>
{
    public CreateOwnerValidator()
    {
        RuleFor(x => x.Email).MustBeValidValueObject(Email.Create);
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
    }
}