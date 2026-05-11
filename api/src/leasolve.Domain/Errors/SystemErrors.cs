using leasolve.Domain.Abstractions;

namespace leasolve.Domain.Errors;

public static class SystemErrors
{
    public static readonly Error InvalidEnvironment = new(ErrorType.Problem, "SystemErrors.InvalidEnvironment", "ASPNETCORE_ENVIRONMENT is missing or empty. Set it in the process or host configuration (e.g. Development, Staging, Production).");
}