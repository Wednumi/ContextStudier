using Ardalis.Specification.EntityFrameworkCore;
using ContextStudier.Core.Entitites;
using Microsoft.EntityFrameworkCore;
using ContextStudier.Core.Interfaces.DataAccess;

namespace ContextStudier.Infrastructure.DataAccess
{
    internal class Repository<T> : RepositoryBase<T>, IRepository<T> where T : BaseEntity
    {
        public Repository(DbContext dbContext)
            : base(dbContext)
        { }
    }
}