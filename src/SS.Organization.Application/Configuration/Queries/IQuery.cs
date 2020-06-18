using MediatR;

namespace SS.Organizations.Application.Configuration.Queries
{
    public interface IQuery<Result> : IRequest<Result> 
    {
    }
}
