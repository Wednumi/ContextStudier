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

        public async Task<Folder> UpdateAsync(Folder folder, CancellationToken cancellationToken = default)
        {
            var updatableFolder = await GetUpdatableFolderAsync(folder, cancellationToken);

            var updated = _mapper.Map(folder, updatableFolder);
            await _repository.UpdateAsync(updated, cancellationToken);
            
            return updated;
        }

        private async Task<Folder> GetUpdatableFolderAsync(Folder folder, 
            CancellationToken cancellationToken = default)
        {
            if (folder.Id == default)
            {
                return folder;
            }

            return await GetFolderAsync(folder.Id, folder.UserId, cancellationToken);
        }

        public async Task<Folder> GetFolderAsync(int folderId, string userId,
            CancellationToken cancellationToken = default)
        {
            var storedFolder = await _repository.GetByIdAsync(folderId, cancellationToken);

            if (storedFolder is null)
            {
                throw new EntityNotFoundException();
            }

            if (userId != storedFolder.UserId)
            {
                throw new NotAllowedForRequesterException();
            }

            return storedFolder;
        }
    }
}
