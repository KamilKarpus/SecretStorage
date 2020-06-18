using Autofac;
using SS.Common.DomainEvents;
using SS.Common.EventDispatchers;
using SS.Organizations.Application.Commands;

namespace SS.Organizations.Infrastructure.Configuration.Proccesing
{
    public class ProccesingModule : Autofac.Module
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
