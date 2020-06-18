using Autofac;
using MediatR;
using SS.Common.DomainEvents;
using SS.Common.EventDispatchers;
using SS.Users.Application.Configuration.Commands;

namespace SS.Users.Infrastructure.Configuration.Proccesing
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
