using System;

namespace SS.IntegrationTests
{
    public class Organization
    {
        public static class Post
        {
            public static string AddOrganization => "/api/organization";
            public static string AddUserToOrganization(Guid id) => $"/api/organization/{id}/users";
        }
        public static class Delete
        {
            public static string DeleteUserFromOrganization(Guid organizationId, Guid userId)
                => $"/api/organization/{organizationId}/users/{userId}";
            public static string DeleteOrganization(Guid organizationId) => $"/api/organization/{organizationId}";
        }
        public static class Put
        {
            public static string ChangeRole(Guid organizationId, Guid userId) => $"/api/organization/{organizationId}/users/{userId}";
        }
        public static class Get
        {
            public static string GetOrganizationbyId(Guid id) => $"/api/organization/{id.ToString()}";
            
        }
    }
}
