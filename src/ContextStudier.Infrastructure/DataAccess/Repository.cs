using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ContextStudier.Core.Interfaces.DataAccess;
using ContextStudier.Core.Entitites;

namespace ContextStudier.Infrastructure.DataAccess
{
    internal class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class
    {
        internal Repository(DbContext dbContext)
            : base(dbContext)
        { }
    }
}