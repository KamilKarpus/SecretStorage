using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS.Api.Utilies.Filters;
using SS.Collections.Application;
using SS.Collections.Application.AddCollection;
using SS.Collections.Application.Commands.RemoveCollection;
using SS.Collections.Application.Commands.RemoveResource;
using SS.Collections.Application.Configuration;
using SS.Collections.Application.EncryptedResource;
using SS.Collections.Application.GetCollectionInfoView;
using SS.Collections.Application.GetCollectionShortView;
using SS.Collections.Application.GetResourceInfoView;
using SS.Collections.Application.Logs;
using SS.Collections.Application.ReadModels;
using SS.Collections.Application.Resource;
using SS.Collections.Application.UpdateResource;
using SS.Infrastructure.PagginationList;
using SS.Users.Infrastructure.Configuration.Auth.Claims;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace SS.Api.Modules.CollectionsApi
{
    [Authorize]
    [ApiController, Route("api/{organizationId}/collections")]
    public class CollectionsController : Controller
    {
        private readonly ICollectionsModule _module;
        public CollectionsController(ICollectionsModule module)
        {
            _module = module;
        }
        [HttpPost]
        [RoleFilter(Claim = RolesRights.CanEditCollection)]
        [SwaggerOperation(Summary = "Create new collection")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNewCollection(Guid organizationId, [FromBody]ApiCommands.V1.AddCollection collection)
        {
            var collectionId = Guid.NewGuid();
            await _module.ExecuteCommand(new AddCollectionCommand(collectionId, organizationId, collection.Name));
            return Created("api/collections/", new { id = collectionId });
        }
        [HttpPost("{collectionId}/resource")]
        [SwaggerOperation(Summary = "Create new resource")]
        [RoleFilter(Claim = RolesRights.CanEditCollection)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNewResource(Guid collectionId,
            [FromBody]ApiCommands.V1.AddResource resource)
        {
            var resourceId = Guid.NewGuid();
            await _module.ExecuteCommand(new AddResourceCommand()
            {
                CollectionId = collectionId,
                OwnerId = User.GetUserId(),
                OwnerName = User.GetUserDisplayName(),
                Id = resourceId,
                Name = resource.Name,
                Resource = resource.Resource
            });
            return Created($"api/{collectionId}/collections/", new { id = resourceId });
        }

        [HttpGet]
        [RoleFilter(Claim = RolesRights.CanReadCollection)]
        [SwaggerOperation(Summary = "Get collections by organization id")]
        [ProducesResponseType(typeof(PagedList<CollectionShortView>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCollectionbyOrganizationId(Guid organizationId, [FromQuery]ApiQueries.V1.CollectionbyOrganizationId query)
        {
            var result = await _module.ExecuteQuery<PagedList<CollectionShortView>>(new GetCollectionShortViewQuery()
            {
                OrganizationId = organizationId,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber
            });
            return Ok(result);
        }

        [HttpGet("{collectionId}")]
        [RoleFilter(Claim = RolesRights.CanReadCollection)]
        [SwaggerOperation(Summary = "Get collection by collection id")]
        [ProducesResponseType(typeof(CollectionInfoView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCollectionbyId(Guid collectionId)
        {
            var result = await _module.ExecuteQuery<CollectionInfoView>(new GetCollectionInfoViewQuery()
            {
                CollectionId = collectionId
            });
            return Ok(result);
        }

        [HttpGet("{collectionId}/resource/{resourceId}")]
        [RoleFilter(Claim = RolesRights.CanReadCollection)]
        [SwaggerOperation(Summary = "Get resource info by resource id")]
        [ProducesResponseType(typeof(ResourceInfoView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetResourcebyId(Guid collectionId, Guid resourceId)
        {
            var result = await _module.ExecuteQuery<ResourceInfoView>(new GetResourceInfoViewQuery()
            {
                ResourceId = resourceId,
                CollectionId = collectionId
            });
            return Ok(result);
        }
        [HttpGet("{collectionId}/resource/{resourceId}/encrypted")]
        [SwaggerOperation(Summary = "Get encrypted value by id")]
        [RoleFilter(Claim = RolesRights.CanReadCollection)]
        [ProducesResponseType(typeof(EncryptedValue), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEncryptedResourcebyId(Guid collectionId, Guid resourceId)
        {
            var result = await _module.ExecuteCommandAsync<EncryptedValue>(new GetEncryptedResourceCommand()
            {
                CollectionId = collectionId,
                ResourceId = resourceId,
                DisplayName = User.GetUserDisplayName(),
                UserId = User.GetUserId()
            });
            return Ok(result);
        }
        [HttpGet("resource/{resourceId}/logs")]
        [RoleFilter(Claim = RolesRights.CanReadCollection)]
        [SwaggerOperation(Summary = "Get logs for resource by id")]
        [ProducesResponseType(typeof(PagedList<LogsView>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEncryptedResourcebyId(Guid resourceId,
            [FromQuery] ApiQueries.V1.PagedList pagedList)
        {
            var result = await _module.ExecuteQuery<PagedList<LogsView>>(new GetLogsQuery()
            {
                ResourceId = resourceId,
                PageSize = pagedList.PageSize,
                PageNumber = pagedList.PageNumber
            });
            return Ok(result);
        }
        [HttpPut("{collectionId}/resource/{resourceId}")]
        [RoleFilter(Claim = RolesRights.CanEditCollection)]
        [SwaggerOperation(Summary = "Update encrypted value")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEncryptedValue(Guid collectionId, Guid resourceId,
            [FromBody]ApiCommands.V1.UpdateResource resource)
        {
            await _module.ExecuteCommand(new UpdateResourceCommand()
            {
                Value = resource.Resource,
                CollectionId = collectionId,
                ResourceId = resourceId,
                DisplayName = User.GetUserDisplayName(),
                UserId = User.GetUserId()
            });
            return Ok();
        }
        [HttpDelete("{collectionId}")]
        [RoleFilter(Claim = RolesRights.CanEditOrganization)]
        [SwaggerOperation(Summary = "Delete Collection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteOrganization(Guid collectionId)
        {
            await _module.ExecuteCommand(new RemoveCollectionCommand()
            {
                CollectionId = collectionId
            });
            return Ok(); 
        }

        [HttpDelete("{collectionId}/resourse/{resourceId}")]
        [RoleFilter(Claim = RolesRights.CanEditOrganization)]
        [SwaggerOperation(Summary = "Delete resource by Resource Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteResource(Guid collectionId, Guid resourceId)
        {
            await _module.ExecuteCommand(new RemoveResourceCommand()
            {
                CollectionId = collectionId,
                ResourceId = resourceId
            });
            return Ok();
        }
    }
}