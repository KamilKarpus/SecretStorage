using System;

namespace SS.Collections.Application.Configuration
{
    public static class ApiCommands
    {
        public static class V1
        {
            public class AddCollection
            {
                public string Name { get; set; }
            }

            public class AddResource
            {
                public string Name { get; set; }
                public string Resource { get; set; }
            }
            public class UpdateResource
            {
                public string Resource { get; set; }
            }
        }
    }
}
