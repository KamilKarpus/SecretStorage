using System;

namespace SS.Organizations.Application.Commands.AddUserToOrganization
{
    public class AddUserToOrganizationCommand : ICommand
    {
        public Guid OrganizationId { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
    }
}
