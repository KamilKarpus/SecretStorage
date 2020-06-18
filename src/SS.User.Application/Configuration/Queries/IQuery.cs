using MediatR;

namespace SS.Users.Application.Configuration.Queries
{
    public interface IQuery<Result> : IRequest<Result> 
    {
    }
}
