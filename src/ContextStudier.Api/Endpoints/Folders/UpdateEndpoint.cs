using Ardalis.ApiEndpoints;
using AutoMapper;
using ContextStudier.Core.Entitites;
using ContextStudier.Core.Exceptions;
using ContextStudier.Core.Services;
using ContextStudier.Presentation.Core.EntitiesModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ContextStudier.Api.Endpoints.Folders
{
    public class CreateEndpoint : EndpointBaseAsync
        .WithRequest<FolderModel>
        .WithActionResult<FolderModel>
    {
        private readonly FolderService _folderService;

        private readonly IMapper _mapper;

        public CreateEndpoint(FolderService folderService, IMapper mapper)
        {
            _folderService = folderService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("api/folder/update")]
        [SwaggerOperation(Tags = new[] { "Folders" })]
        public override async Task<ActionResult<FolderModel>> HandleAsync(FolderModel folderModel,
            CancellationToken cancellationToken = default)
        {
            var folder = new Folder(this.GetUserId());
            _mapper.Map(folderModel, folder);

            try
            {
                var updated = await _folderService.UpdateAsync(folder, cancellationToken);
                return _mapper.Map<FolderModel>(updated);
            }
            catch (NotAllowedForRequesterException)
            {
                return BadRequest("You are not allowed to update this folder");
            }
        }
    }
}
