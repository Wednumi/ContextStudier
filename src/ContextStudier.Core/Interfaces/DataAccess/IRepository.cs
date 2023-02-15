using Ardalis.Specification;
using ContextStudier.Core.Entitites;

namespace ContextStudier.Core.Interfaces.DataAccess
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class
    {
    }
}
