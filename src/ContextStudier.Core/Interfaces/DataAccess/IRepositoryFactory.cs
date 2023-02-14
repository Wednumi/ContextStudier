using ContextStudier.Core.Entitites;

namespace ContextStudier.Core.Interfaces.DataAccess
{
    public interface IRepositoryFactory
    {
        public IRepository<T> GetRepository<T>() where T : BaseEntity;
    }
}
