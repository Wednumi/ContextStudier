using ContextStudier.Core.Entitites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContextStudier.Infrastructure.DataAccess
{
    internal class ApplicationContext : IdentityDbContext<User>
    {
        private IConfiguration _config;

        internal DbSet<User> Users { get; set; }

        internal DbSet<Folder> Folders { get; set; }

        internal DbSet<StudyPair> StudyPairs { get; set; }

        public ApplicationContext(IConfiguration config)
        {
            _config = config;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _config.GetConnectionString("Default");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}