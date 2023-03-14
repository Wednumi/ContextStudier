using AutoMapper;
using ContextStudier.Core.Entitites;
using ContextStudier.Core.Interfaces.DataAccess;

namespace ContextStudier.Core.Services
{
    public class FolderService
    {
        private readonly IRepository<Folder> _repository;

        private readonly IMapper _mapper;

        public FolderService(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repository = repositoryFactory.GetRepository<Folder>();
            _mapper = mapper;
        }

        public async Task<Folder> UpdateAsync(Folder folder)
        {
            var updatableFolder = await GetUpdatableFolderAsync(folder);

            var updated = _mapper.Map(folder, updatableFolder);
            await _repository.UpdateAsync(updated);
            
            return updated;
        }

        private async Task<Folder> GetUpdatableFolderAsync(Folder folder)
        {
            if (folder.Id == default)
            {
                return folder;
            }

            var storedFolder = await _repository.GetByIdAsync(folder.Id);

            if(storedFolder is null)
            {
                throw new EntityWasNotStoredException();
            }

            if(folder.UserId != storedFolder.UserId)
            {
                throw new NotAllowedForRequesterException();
            }

            return storedFolder;
        }
    }
}
