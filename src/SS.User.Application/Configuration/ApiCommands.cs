namespace SS.Users.Application.Configuration
{
    public static class ApiCommands
    {
        public static class V1
        {
            public class RegisterUser
            {
                public string Email { get; set; }
                public string Password { get; set; }
                public string DisplayName { get; set; }
            }
            public class LoginUser
            {
                public string Email { get; set; }
                public string Password { get; set; }
            }
            public class RefreshToken
            {
                public string Token { get; set; }
            }
        } 
    }
}
