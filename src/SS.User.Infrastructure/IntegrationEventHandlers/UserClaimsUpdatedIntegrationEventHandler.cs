using MediatR;
using SS.Infrastructure.GrantStore;
using SS.Users.Domain.Repositories;
using SS.Users.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Users.Infrastructure.IntegrationEventsHandlers
{
    public class UserClaimsUpdatedIntegrationEventHandler : INotificationHandler<UserClaimsUpdatedIntegrationEvent>
    {
        private readonly IUserRepository _repository;
        private readonly IGrantStore _store;
        public UserClaimsUpdatedIntegrationEventHandler(IUserRepository repository, IGrantStore store)
        {
            _repository = repository;
            _store = store;
        }
        public async Task Handle(UserClaimsUpdatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var user = await _repository.GetbyId(notification.UserId);
            user.UpdateOrganizationClaims(notification.OrganizationId, notification.Claims);
            await _repository.Update(user);
            await _store.DeleteUserGrant(notification.UserId);

        }
    }
}
