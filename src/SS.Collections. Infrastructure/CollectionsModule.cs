using Autofac;
using MediatR;
using SS.Collections.Infrastructure.Configuration;
using SS.Collections.Application;
using SS.Collections.Application.Configuration.Commands;
using SS.Collections.Application.Configuration.Queries;
using System.Threading.Tasks;

namespace SS.Collections.Infrastructure
{
    public class CollectionModule : ICollectionsModule
    {
        public async Task ExecuteCommand(ICommand command)
        {
            using (var scope = CollectionsCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(command);
            }
        }

        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            using (var scope = CollectionsCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(command);
            }
        }

        public async Task<TResult> ExecuteQuery<TResult>(IQuery<TResult> query)
        {
            using (var scope = CollectionsCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(query);
            }
        }
    }
}
