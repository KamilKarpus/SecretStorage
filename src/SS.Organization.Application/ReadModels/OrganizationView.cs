using System;
using System.Collections.Generic;

namespace SS.Organizations.Application.ReadModels.Organizations
{
    public class OrganizationView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<UserView> Users { get; set; }
    }
}
