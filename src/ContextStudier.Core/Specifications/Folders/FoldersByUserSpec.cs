using Ardalis.Specification;
using ContextStudier.Core.Entitites;

namespace ContextStudier.Core.Specifications.Folders
{
    public class FolderByUserSpec : Specification<Folder>
    {
        public FolderByUserSpec(string userId)
        {
            Query.Where(f => f.UserId == userId);
        }
    }
}
