using ContextStudier.Core.Entitites;
using ContextStudier.Core.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ContextStudier.Infrastructure.DataAccess
{
    internal class RepositoryFactory : IRepositoryFactory
    {
        private readonly DbContext _dbContext;

        public RepositoryFactory(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_dbContext);
        }
    }
}
