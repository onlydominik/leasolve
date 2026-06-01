using leasolve.Domain.Users;
using leasolve.Domain.Users.Enums;
using leasolve.Infrastructure.Settings;
using leasolve.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace leasolve.Infrastructure.Services;

internal sealed class DataSeederService : IDataSeederService
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly DataSeederSettings _dataSeederSettings;

    public DataSeederService(
        RoleManager<IdentityRole<long>> roleManager,
        UserManager<User> userManager,
        IOptions<DataSeederSettings> dataSeederSettings)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _dataSeederSettings = dataSeederSettings.Value;
    }

    public async Task Seed()
    {
        await SeedRolesAsync();

        if (_dataSeederSettings.SeedData)
            await SeedAdminUserAsync();
    }

    private async Task SeedRolesAsync()
    {
        var validRoleNames = Enum.GetNames<Roles>().ToHashSet();

        var rolesToDelete = _roleManager.Roles
            .Where(r => !validRoleNames.Contains(r.Name!))
            .ToList();

        foreach (var role in rolesToDelete)
            await _roleManager.DeleteAsync(role);

        var currentRoleNames = _roleManager.Roles
            .Select(r => r.Name)
            .ToHashSet();

        foreach (var role in Enum.GetValues<Roles>())
        {
            var roleName = role.ToString();
            if (!currentRoleNames.Contains(roleName))
                await _roleManager.CreateAsync(new IdentityRole<long>(roleName));
        }
    }

    private async Task SeedAdminUserAsync()
    {
        if (_dataSeederSettings.AdminUser is null)
            throw new InvalidOperationException("AdminUser settings are not configured.");

        var existingUser = await _userManager.FindByEmailAsync(_dataSeederSettings.AdminUser.Email!);
        if (existingUser is not null)
            return;

        var userResult = User.Create(_dataSeederSettings.AdminUser.Email!, "Main", "Admin");

        if (userResult.IsFailure)
            throw new InvalidOperationException($"Failed to create admin user: {userResult.Error.Details}");

        var identityResult = await _userManager.CreateAsync(userResult.Value, _dataSeederSettings.AdminUser.Password!);
        //TODO: add role
        if (!identityResult.Succeeded)
            throw new InvalidOperationException($"Failed to create admin user: {identityResult.Errors.First().Description}");
    }
}