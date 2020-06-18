using SS.Organizations.Application.Commands;
using System;

namespace SS.Organizations.Application.RemoveUserFromOrganization
{
    public class RemoveUserFromOrganizationCommand : ICommand
    {
        public Guid OrganizationId { get; set; }
        public Guid UserIdToDelete { get; set; }
        public Guid RequestingUserId { get; set; }
    }
}
