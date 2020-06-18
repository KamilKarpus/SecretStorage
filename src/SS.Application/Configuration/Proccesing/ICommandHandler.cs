using MediatR;

namespace SS.Application.Configuration.Proccesing
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
    {

    }
}
