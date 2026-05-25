using MediatR;

namespace leasolve.Application.Abstractions.CQRS;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : ICommand<TResponse>
{
}