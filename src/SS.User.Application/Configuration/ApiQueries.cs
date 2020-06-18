namespace SS.Users.Application.Configuration
{
    public static class ApiQueries
    {
        public static class V1
        {
            public class PagginationRequest
            {
                public int PageNumber { get; set; }
                public int PageSize { get; set; }
            }
        }
    }
}
