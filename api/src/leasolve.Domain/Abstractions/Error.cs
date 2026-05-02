namespace leasolve.Domain.Abstractions;

public sealed record Error(ErrorType Type, string Code, string Details)
{
    public static readonly Error None = new(ErrorType.None, string.Empty, string.Empty);
}