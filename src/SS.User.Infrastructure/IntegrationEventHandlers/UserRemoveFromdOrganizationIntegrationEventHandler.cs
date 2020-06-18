using MediatR;
using SS.Infrastructure.EventBus;
using SS.Infrastructure.GrantStore;
using SS.Users.Domain.Repositories;
using SS.Users.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Users.Infrastructure.IntegrationEventsHandlers
{
    public class UserRemoveFromdOrganizationIntegrationEventHandler : INotificationHandler<UserRemovedFromOrganizationIntegrationEvent>
    {
        private readonly IUserRepository _repository;
        private readonly IGrantStore _store;
        public UserRemoveFromdOrganizationIntegrationEventHandler(IUserRepository repository
            , IGrantStore store)
        {
            _repository = repository;
            _store = store;
        }
        public async Task Handle(UserRemovedFromOrganizationIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var user = await _repository.GetbyId(notification.UserId);
            if (user != null)
            {
                user.RemoveOrganization(notification.OrganizationId);
                await _repository.Update(user);
                await _store.DeleteUserGrant(notification.UserId);
            }
        }
    }
}
