using leasolve.Application.Abstractions.CQRS;
using leasolve.Domain.Abstractions;

namespace leasolve.Application.Features.Users.Users.Commands.CreateOwner;

public sealed record CreateOwnerCommand(string Email, 
                                        string FirstName, 
                                        string LastName, 
                                        string Password) : ICommand<Result<long>>;