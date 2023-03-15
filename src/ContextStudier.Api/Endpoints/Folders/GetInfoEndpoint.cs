using Ardalis.ApiEndpoints;
using AutoMapper;
using ContextStudier.Core.Entitites;
using ContextStudier.Core.Interfaces.DataAccess;
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
        private readonly IRepository<Folder> _repository;

        private readonly IMapper _mapper;

        public GetInfoEndpoint(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repository = repositoryFactory.GetRepository<Folder>();
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("api/folder/info")]
        [SwaggerOperation(Tags = new[] { "Folders" })]
        public override async Task<ActionResult<FolderInfo>> HandleAsync(int folderId,
            CancellationToken cancellationToken = default)
        {
            var folder = await _repository.GetByIdAsync(folderId, cancellationToken);
            return _mapper.Map<FolderInfo>(folder);
        }
    }
}
