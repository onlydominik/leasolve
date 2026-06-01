using leasolve.Application.Abstractions.CQRS;
using leasolve.Domain.Abstractions;

namespace leasolve.Application.Features.Users.Users.Commands.CreateTenant;

// TODO: Tenent user must have auto generated password with email-send
public sealed record CreateTenantCommand(string Email, 
                                         string FirstName, 
                                         string LastName,
                                         string Password) : ICommand<Result<long>>;