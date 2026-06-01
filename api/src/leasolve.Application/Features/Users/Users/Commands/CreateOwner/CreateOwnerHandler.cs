using leasolve.Application.Abstractions;
using leasolve.Application.Abstractions.CQRS;
using leasolve.Domain.Abstractions;
using leasolve.Domain.Users;
using leasolve.Domain.Users.Enums;

namespace leasolve.Application.Features.Users.Users.Commands.CreateOwner;

internal sealed class CreateOwnerHandler : ICommandHandler<CreateOwnerCommand, Result<long>>
{
    private readonly IUserService _userService;
    
    public CreateOwnerHandler(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<Result<long>> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
    {
        if (await _userService.IsUserExistsByEmailAsync(request.Email))
            return UsersErrors.EmailAlreadyExists;

        var newUserResult = User.Create(
            request.Email,
            request.FirstName,
            request.LastName);

        if (newUserResult.IsFailure) return newUserResult.Error;

        User user = newUserResult.Value;
        
        var identityResult = await _userService.CreateUserAsync(user, request.Password);
        
        if (identityResult.IsFailure) return identityResult.Error;

        var assignToRoleResult = await _userService.AssignRoleToUserAsync(user, Roles.Owner);

        if (assignToRoleResult.IsFailure) return assignToRoleResult.Error;

        return user.Id;
    }
}