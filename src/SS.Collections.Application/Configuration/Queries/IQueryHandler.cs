using MediatR;

namespace SS.Collections.Application.Configuration.Queries
{
    internal interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
    }
}
