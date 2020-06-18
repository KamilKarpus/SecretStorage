using MediatR;
using SS.Common.DomainEvents;
using SS.Infrastructure.EventBus;
using SS.Infrastructure.GrantStore;
using SS.Users.Domain.Repositories;
using SS.Users.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Users.Infrastructure.IntegrationEventsHandlers
{
    public class UserAddToOrganizationIntegrationEventHandler : INotificationHandler<UserAddToOrganizationIntegrationEvent>
    {
        private readonly IUserRepository _repository;
        private readonly IGrantStore _store;
        public UserAddToOrganizationIntegrationEventHandler(IUserRepository repository,
            IGrantStore store)
        {
            _repository = repository;
            _store = store;
        }
        public async Task Handle(UserAddToOrganizationIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var user = await _repository.GetbyId(notification.UserId);
            user.AddOrganization(notification.OrganizationId, notification.Claims);
            await _repository.Update(user);
            await _store.DeleteUserGrant(notification.UserId);
        }
    }
}
