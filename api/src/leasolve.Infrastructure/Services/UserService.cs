using leasolve.Application.Abstractions;
using leasolve.Domain.Abstractions;
using leasolve.Domain.Users;
using leasolve.Domain.Users.Enums;
using Microsoft.AspNetCore.Identity;

namespace leasolve.Infrastructure.Services;

internal sealed class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public UserService(UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager)
    {
        _userManager = userManager;
        _roleManager =  roleManager;
    } 
    
    public async Task<bool> IsUserExistsByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email) is not null;
    }
    
    public async Task<Result> CreateUserAsync(User user, string password)
    {
        var identityResult = await _userManager.CreateAsync(user, password);

        if (identityResult.Succeeded)
            return Result.Success();
        
        // TODO: logger

        if (identityResult.Errors.Any(e => e.Code is nameof(IdentityErrorDescriber.DuplicateEmail) or nameof(IdentityErrorDescriber.DuplicateUserName)))
            return UsersErrors.EmailAlreadyExists;

        return UsersErrors.RegistrationFailed;
    }

    public async Task<Result> AssignRoleToUserAsync(User user, Roles role)
    {
        var identityRole = await _roleManager.FindByNameAsync(role.ToString());

        if (identityRole is null)
            return UsersErrors.RoleNotFound;
        
        var identityRoleResult = await _userManager.AddToRoleAsync(user, role.ToString());
        
        if (!identityRoleResult.Succeeded)
            return UsersErrors.RoleAssignmentFailed;
        
        return Result.Success();
    }
}