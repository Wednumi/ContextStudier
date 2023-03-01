using ContextStudier.Core.Entitites;
using ContextStudier.Core.Specifications.Folders;
using ContextStudier.Core.Interfaces.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ContextStudier.Presentation.Core.EntitiesModels;
using AutoMapper;

namespace ContextStudier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly IRepository<Folder> _repository;

        private readonly IMapper _mapper;

        public FolderController(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repository = repositoryFactory.GetRepository<Folder>();
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<FolderModel>> Get()
        {
            var folders = await _repository.ListAsync(new FolderByUserSpec(this.GetUserId()));
            return _mapper.Map<List<FolderModel>>(folders);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(FolderModel folderModel)
        {
            if(ModelState.IsValid is false)
            {
                return BadRequest(ModelState);
            }

            var folder = new Folder(this.GetUserId());
            _mapper.Map(folderModel, folder);

            if(await CanUpdate(folder))
            {
                await _repository.UpdateAsync(folder);
                var updatedModel = _mapper.Map<FolderModel>(folder);
                return Ok(updatedModel);                
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<bool> CanUpdate(Folder folder)
        {
            if(folder.Id == default)
            {
                return true;
            }

            var storedFolder = await _repository.GetByIdAsync(folder.Id);
            var userId = this.GetUserId();

            return storedFolder is not null &&
                storedFolder.UserId == userId;
        }
    }
}
