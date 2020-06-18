using Autofac;
using MediatR;
using SS.Organizations.Application;
using SS.Organizations.Application.Commands;
using SS.Organizations.Application.Configuration.Queries;
using System.Threading.Tasks;

namespace SS.Organizations.Infrastructure.Configuration.ModuleExecution
{
    public class OrganizationModule : IOrganizationModule
    {
        public async Task ExecuteCommand(ICommand command)
        {
            using (var scope = OrganizationCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(command);
            }
        }

        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            using (var scope = OrganizationCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(command);
            }
        }

        public async Task<TResult> ExecuteQuery<TResult>(IQuery<TResult> query)
        {
            using(var scope = OrganizationCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(query);
            }
        }
    }
}
