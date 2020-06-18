namespace SS.Organizations.Application.Configuration
{
    public static class ApiCommands
    {
        public static class V1
        {
            public class AddOrganization
            {
                public string Name { get; set; }
            }
            public class AddUserToOrganization
            {
                public string Email { get; set; }
            }
            public class ChangeUserRole
            {
                public int RoleId { get; set; }
            }
        } 
    }
}
