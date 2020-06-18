using System;

namespace SS.Organizations.Application.Commands.ChangeUserRole
{
    public class ChangeUserRoleCommand : ICommand
    {
        public Guid OrganizationId { get; set; }
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}
