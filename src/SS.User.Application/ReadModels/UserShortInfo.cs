using System;

namespace SS.Users.Application.ReadModels
{
    public class UserShortInfo
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }
}
