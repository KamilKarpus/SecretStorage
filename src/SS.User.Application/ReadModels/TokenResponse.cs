﻿namespace SS.Users.Application.ReadModels
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
    }
}
