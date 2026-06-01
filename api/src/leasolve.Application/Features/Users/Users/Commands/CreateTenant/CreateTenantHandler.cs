using leasolve.Application.Abstractions;
using leasolve.Application.Abstractions.CQRS;
using leasolve.Domain.Abstractions;
using leasolve.Domain.Users;
using leasolve.Domain.Users.Enums;

namespace leasolve.Application.Features.Users.Users.Commands.CreateTenant;

internal sealed class CreateTenantHandler : ICommandHandler<CreateTenantCommand, Result<long>>
{
    private readonly IUserService _userService;
    
    public CreateTenantHandler(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<Result<long>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        if (await _userService.IsUserExistsByEmailAsync(request.Email))
            return UsersErrors.EmailAlreadyExists;

        var newUserResult = User.Create(
            request.Email,
            request.FirstName,
            request.LastName);

        if (newUserResult.IsFailure) return newUserResult.Error;

        User user = newUserResult.Value;
        
        // TODO: Tenent user must have auto generated password with email-send
        var identityResult = await _userService.CreateUserAsync(user, request.Password);
        
        if (identityResult.IsFailure) return identityResult.Error;

        var assignToRoleResult = await _userService.AssignRoleToUserAsync(user, Roles.Tenant);

        if (assignToRoleResult.IsFailure) return assignToRoleResult.Error;

        return user.Id;
    }
}