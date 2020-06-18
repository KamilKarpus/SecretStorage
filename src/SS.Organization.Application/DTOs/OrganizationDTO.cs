using System;

namespace SS.Organizations.Application.DTOs
{
    public class OrganizationDTO
    {
        public Guid Id { get; set; }
        public string[] Claims { get; set; }
    }
}
