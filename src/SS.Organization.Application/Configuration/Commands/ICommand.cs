using MediatR;

namespace SS.Organizations.Application.Commands
{
    public interface ICommand : IRequest
    {

    }
    public interface ICommand<TReponse> : IRequest<TReponse>
    {

    }
}
