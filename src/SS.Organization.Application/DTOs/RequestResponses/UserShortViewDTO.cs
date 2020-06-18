using System;

namespace SS.Organizations.Application.DTOs
{
    public class UserShortViewDTO
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }
}
