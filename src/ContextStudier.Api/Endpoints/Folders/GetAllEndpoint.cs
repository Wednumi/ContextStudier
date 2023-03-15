using Ardalis.ApiEndpoints;
using AutoMapper;
using ContextStudier.Core.Entitites;
using ContextStudier.Core.Interfaces.DataAccess;
using ContextStudier.Core.Specifications.Folders;
using ContextStudier.Presentation.Core.EntitiesModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ContextStudier.Api.Endpoints.Folders
{
    public class GetAllEndpoint : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<FolderModel>>
    {
        private readonly IRepository<Folder> _repository;

        private readonly IMapper _mapper;

        public GetAllEndpoint(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repository = repositoryFactory.GetRepository<Folder>();
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("api/folder/all")]
        [SwaggerOperation(Tags = new[] { "Folders" })]
        public override async Task<ActionResult<List<FolderModel>>> HandleAsync(
            CancellationToken cancellationToken = default)
        {
            var folders = await _repository.ListAsync(new FolderByUserSpec(this.GetUserId()),
                cancellationToken);
            return _mapper.Map<List<FolderModel>>(folders);
        }
    }
}
