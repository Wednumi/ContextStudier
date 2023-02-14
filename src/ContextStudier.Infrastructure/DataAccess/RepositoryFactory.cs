using ContextStudier.Core.Entitites;
using ContextStudier.Core.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ContextStudier.Infrastructure.DataAccess
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly DbContext _dbContext;

        public RepositoryFactory(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_dbContext);
        }
    }
}
