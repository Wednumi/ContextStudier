using Ardalis.ApiEndpoints;
using AutoMapper;
using ContextStudier.Core.Entitites;
using ContextStudier.Core.Exceptions;
using ContextStudier.Core.Interfaces.DataAccess;
using ContextStudier.Core.Services;
using ContextStudier.Presentation.Core.EntitiesModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ContextStudier.Api.Endpoints.Folders
{
    public class GetInfoEndpoint : EndpointBaseAsync
        .WithRequest<int>
        .WithActionResult<FolderInfo>
    {
        private readonly FolderService _folderService;

        private readonly IMapper _mapper;

        public GetInfoEndpoint(FolderService folderService, IMapper mapper)
        {
            _folderService = folderService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("api/folder/info")]
        [SwaggerOperation(Tags = new[] { "Folders" })]
        public override async Task<ActionResult<FolderInfo>> HandleAsync(int folderId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var folder = await _folderService.GetFolderAsync(folderId, this.GetUserId(), 
                    cancellationToken);
                return _mapper.Map<FolderInfo>(folder);
            }
            catch(EntityNotFoundException)
            {
                return BadRequest("Folder was not found");
            }
            catch (NotAllowedForRequesterException)
            {
                return BadRequest("You are not allowed to view this folder");
            }            
        }
    }
}
