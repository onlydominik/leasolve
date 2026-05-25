using MediatR;

namespace leasolve.Application.Abstractions.CQRS;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}