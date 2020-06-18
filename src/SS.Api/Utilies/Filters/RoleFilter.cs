using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SS.Infrastructure.GrantStore;
using SS.Users.Infrastructure.Configuration.Auth.Claims;
using System;
using SS.Users.Infrastructure.Configuration;
using Autofac;
using System.Net;

namespace SS.Api.Utilies.Filters
{
    public class RoleFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly IGrantStore _store;
        public string Claim { get; set; }

        public RoleFilter()
        {
            _store = UserCompositionRoot.BeginLifetimeScope().Resolve<IGrantStore>();
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;
            var invalidToken = _store.HasUserHaveValidToken(httpContext.User.GetUserId()).Result;
            if (!invalidToken)
            {
                context.Result =  Result(HttpStatusCode.Unauthorized, "Invalid token");
            }
            var organizationId = GetOrganizationIDFromURL(httpContext);
            if (!httpContext.User.HasUsersClaim(organizationId, Claim))
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private Guid GetOrganizationIDFromURL(HttpContext context)
        {
            var requestedPath = context.Request.Path.Value.Split('/');
            foreach (var segment in requestedPath)
            {
                Guid guid;
                if (Guid.TryParse(segment, out guid))
                    return guid;
            }
            return Guid.Empty;
        }
        private ActionResult Result(HttpStatusCode statusCode, string reason) => new ContentResult
        {
            StatusCode = (int)statusCode,
            Content = $"Status Code: {(int)statusCode}; {statusCode}; {reason}",
            ContentType = "text/plain",
        };
    }
}
