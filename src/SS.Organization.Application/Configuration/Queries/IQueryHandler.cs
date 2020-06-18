using MediatR;

namespace SS.Organizations.Application.Configuration.Queries
{
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery,TResult> where TQuery : IQuery<TResult>
    {
    }
}
