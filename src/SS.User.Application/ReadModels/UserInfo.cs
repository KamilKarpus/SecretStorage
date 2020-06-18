using System;
using System.Collections.Generic;

namespace SS.Users.Application.ReadModels
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public List<OrganizationInfo> Organizations { get; set; }
    }

    public class OrganizationInfo
    {
        public Guid Id {get; set;}
        public string[] Claims { get; set; }
    }
}
