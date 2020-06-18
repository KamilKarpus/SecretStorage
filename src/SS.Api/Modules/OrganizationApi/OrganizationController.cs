using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;
using SS.Infrastructure.PagginationList;
using SS.Organizations.Application;
using SS.Organizations.Application.Commands.Organizations;
using SS.Organizations.Application.Commands.AddUserToOrganization;
using SS.Organizations.Application.Commands.ChangeUserRole;
using SS.Organizations.Application.Commands.RemoveOrganization;
using SS.Organizations.Application.ReadModels.Organizations;
using SS.Organizations.Application.Configuration;
using SS.Users.Infrastructure.Configuration.Auth.Claims;
using SS.Organizations.Application.RemoveUserFromOrganization;
using SS.Organizations.Application.Queries;
using SS.Api.Utilies.Filters;

namespace SS.Api.Modules.OrganizationApi
{
    [Authorize]
    [ApiController, Route("api/organization")]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationModule _organizationModule;

        public OrganizationController(IOrganizationModule module)
        {
            _organizationModule = module;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new organization")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(ApiCommands.V1.AddOrganization organization)
        {
            var id = Guid.NewGuid();
            var userId = User.GetUserId();
            await _organizationModule.ExecuteCommand(new RegisterOrganizationCommand()
            {
                OrgnizationId = id,
                OrganizationName = organization.Name,
                UserId = userId
            });
            return Created("api/organization/", new { id = id });
        }
        [HttpPost("{id}/users")]
        [SwaggerOperation(Summary = "Add new user to organization")]
        [RoleFilter(Claim = RolesRights.CanEditOrganization)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUser(Guid id, [FromBody]ApiCommands.V1.AddUserToOrganization user)
        {
            var userId = User.GetUserId();
            await _organizationModule.ExecuteCommand(new AddUserToOrganizationCommand()
            {
                Email = user.Email,
                OrganizationId = id,
                UserId = userId
            });
            return Created($"api/organization/{id}/users/", new { id = userId });
        }

        [HttpPut("{organizationId}/users/{userId}")]
        [SwaggerOperation(Summary = "Change user role", Description = "1 - Owner, 2 - Admin, 3 - User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [RoleFilter(Claim = RolesRights.CanEditOrganization)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRole(Guid organizationId, Guid userId, [FromBody]ApiCommands.V1.ChangeUserRole role)
        {
            var currentUserId = User.GetUserId();
            await _organizationModule.ExecuteCommand(new ChangeUserRoleCommand()
            {
                CurrentUserId = currentUserId,
                OrganizationId = organizationId,
                UserId = userId,
                RoleId = role.RoleId
            });
            return Ok();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get list of user organizations")]
        [ProducesResponseType(typeof(PagedList<OrganizationShortView>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserOrganizations([FromQuery] ApiQueries.V1.PagginationRequest request)
        {
            var currentUserId = User.GetUserId();
            var result = await _organizationModule.ExecuteQuery<PagedList<OrganizationShortView>>(new GetUserOrganizationQuery()
            {
                UserId = currentUserId,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            });
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get organization by Id")]
        [ProducesResponseType(typeof(OrganizationView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrganizationById(Guid id)
        {
            var result = await _organizationModule.ExecuteQuery<OrganizationView>(new GetOrganizationInfoQuery()
            {
                Id = id
            });
            return Ok(result);
        }

        [HttpDelete("{organizationId}/users/{userId}")]
        [SwaggerOperation(Summary = "Remove user from organization")]
        [RoleFilter(Claim = RolesRights.CanEditOrganization)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveUserFromOrganization(Guid organizationId, Guid userId)
        {
            var currentUserId = User.GetUserId();
            await _organizationModule.ExecuteCommand(new RemoveUserFromOrganizationCommand()
            {
                OrganizationId = organizationId,
                UserIdToDelete = userId,
                RequestingUserId = currentUserId
            });
            return Ok();
        }

        [HttpDelete("{organizationId}")]
        [SwaggerOperation(Summary = "Remove organization")]
        [RoleFilter(Claim = RolesRights.CanEditOrganization)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveOrganization(Guid organizationId)
        {
            var currentUserId = User.GetUserId();
            await _organizationModule.ExecuteCommand(new RemoveOrganizationCommand() 
            { 
                OrganizationId = organizationId,
                UserId = currentUserId
            });
            return Ok();
        }
        
    }
}