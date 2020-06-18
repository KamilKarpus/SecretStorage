using System;

namespace SS.Organizations.Application.Commands.RemoveOrganization
{
    public class RemoveOrganizationCommand : ICommand
    {
        public Guid OrganizationId { get; set; }
        public Guid UserId { get; set; }
    }
}
