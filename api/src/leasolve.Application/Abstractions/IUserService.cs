using leasolve.Domain.Abstractions;
using leasolve.Domain.Users;
using leasolve.Domain.Users.Enums;

namespace leasolve.Application.Abstractions;

public interface IUserService
{
    Task<bool> IsUserExistsByEmailAsync(string email);
    
    Task<Result> CreateUserAsync(User user, string password);
    
    Task<Result> AssignRoleToUserAsync(User user, Roles role);
}