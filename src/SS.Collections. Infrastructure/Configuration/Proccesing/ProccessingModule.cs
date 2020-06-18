using Autofac;
using SS.Collections.Application.Configuration.Commands;
using SS.Common.DomainEvents;
using SS.Common.EventDispatchers;

namespace SS.Collections.Infrastructure.Configuration.Proccesing
{
    internal class ProccesingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGenericDecorator(
                typeof(LoggingCommnadHandlerDecarator<>),
                typeof(ICommandHandler<>));

            builder.RegisterType<EventDispatcher>()
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(ThisAssembly)
              .AsClosedTypesOf(typeof(IDomainEventHandler<>))
              .InstancePerLifetimeScope()
              .FindConstructorsWith(new AllConstructorFinder());


            base.Load(builder);
        }
    }
}
