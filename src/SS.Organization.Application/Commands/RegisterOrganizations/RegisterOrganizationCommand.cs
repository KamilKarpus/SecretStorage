
using System;

namespace SS.Organizations.Application.Commands.Organizations
{
    public class RegisterOrganizationCommand : ICommand
    {
        public Guid OrgnizationId { get;  set; }
        public Guid UserId { get; set; }
        public string OrganizationName { get;  set; }
    }
}
