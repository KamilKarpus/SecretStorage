using System;

namespace SS.Collections.Application.Configuration
{
    public static class ApiQueries
    {
        public static class V1
        {
            public class CollectionbyOrganizationId
            {
                public int PageNumber { get; set; }
                public int PageSize { get; set; }
            }

            public class PagedList
            {
                public int PageNumber { get; set; }
                public int PageSize { get; set; }
            }
        }
    }
}
