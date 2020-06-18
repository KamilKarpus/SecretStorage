using System;
using System.Collections.Generic;

namespace SS.Organizations.Application.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public List<OrganizationDTO> Organizations { get; set; }
    }
}
