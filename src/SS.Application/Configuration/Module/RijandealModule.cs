using Autofac;
using MediatR;
using SS.Application.Configuration.Proccesing;
using System.Threading.Tasks;


namespace SS.Application.Configuration.Module
{
    public class RijandealModule : IRijandealModule
    {
        public async Task ExecuteCommand(ICommand command)
        {
            using (var scope = RijandealCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(command);
            }
        }

    }
}
