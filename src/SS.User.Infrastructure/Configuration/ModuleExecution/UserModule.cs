using Autofac;
using MediatR;
using SS.Users.Application;
using SS.Users.Application.Configuration.Commands;
using SS.Users.Application.Configuration.Queries;
using System.Threading.Tasks;

namespace SS.Users.Infrastructure.Configuration.ModuleExecution
{
    public class UserModule : IUserModule
    {
        public async Task ExecuteCommand(ICommand command)
        {
            using (var scope = UserCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(command);
            }
        }

        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            using (var scope = UserCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(command);
            }
        }

        public async Task<TResult> ExecuteQuery<TResult>(IQuery<TResult> query)
        {
            using(var scope = UserCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(query);
            }
        }
    }
}
