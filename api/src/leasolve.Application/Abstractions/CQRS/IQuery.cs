using MediatR;

namespace leasolve.Application.Abstractions.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}