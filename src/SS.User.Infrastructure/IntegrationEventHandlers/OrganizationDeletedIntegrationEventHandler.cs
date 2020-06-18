using MediatR;
using SS.Infrastructure.GrantStore;
using SS.Users.Domain.Repositories;
using SS.Users.IntegrationEvents;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Users.Infrastructure.IntegrationEventsHandlers
{
    public class OrganizationDeletedIntegrationEventHandler : INotificationHandler<OrganizationDeletedIntergrationEvent>
    {
        private readonly IUserRepository _repository;
        private readonly IGrantStore _store;
        public OrganizationDeletedIntegrationEventHandler(IUserRepository repository,
            IGrantStore store)
        {
            _repository = repository;
            _store = store;
        }

        public async Task Handle(OrganizationDeletedIntergrationEvent notification, CancellationToken cancellationToken)
        {
            if (notification.UsersIds.Any())
            {
                foreach (var id in notification.UsersIds)
                {
                    var user = await _repository.GetbyId(id);
                    user.RemoveOrganization(notification.OrganizationId);
                    await _repository.Update(user);
                    await _store.DeleteUserGrant(id);
                }
            }
        }
    }
}
