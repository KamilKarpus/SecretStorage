namespace SS.IntegrationTests
{
    public static class Users
    {
        public static class Post
        {
            public static string Register => "api/users/register";
            public static string Login => "api/users/connect/token";
            public static string RefreshToken => "/api/users/connect/token/refresh";
        }
    }
}
