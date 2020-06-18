using MediatR;

namespace SS.Collections.Application.Configuration.Commands
{
    public interface ICommand : IRequest
    {

    }
    public interface ICommand<TReponse> : IRequest<TReponse>
    {

    }
}
