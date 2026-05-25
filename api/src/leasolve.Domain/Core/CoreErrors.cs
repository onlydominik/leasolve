using leasolve.Domain.Abstractions;

namespace leasolve.Domain.Core;

public static class CoreErrors
{
    public static class System
    {
        public static readonly Error InvalidEnvironment = Error.NotFound("SystemErrors.InvalidEnvironment", "ASPNETCORE_ENVIRONMENT is missing or empty. Set it in the process or host configuration (e.g. Development, Staging, Production).");
    }
}