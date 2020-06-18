using Autofac;
using SS.Infrastructure.EventBus;
using SS.Users.IntegrationEvents;

namespace SS.Users.Infrastructure.Configuration.EventBus
{
    public class EventBusStartup
    {
        public static void Initialize()
        {
            var eventBus = UserCompositionRoot.BeginLifetimeScope().Resolve<IEventBus>();
            SubscribeToEventBus<OrganizationDeletedIntergrationEvent>(eventBus);
            SubscribeToEventBus<UserAddToOrganizationIntegrationEvent>(eventBus);
            SubscribeToEventBus<UserClaimsUpdatedIntegrationEvent>(eventBus);
            SubscribeToEventBus<UserRemovedFromOrganizationIntegrationEvent>(eventBus);
        }

        private static void SubscribeToEventBus<T>(IEventBus eventBus)  where T : IntegrationEvent
        {
            eventBus.Subscribe<T>(new IntegrationEventGenericHandler<T>());
        }
       
    }
}
