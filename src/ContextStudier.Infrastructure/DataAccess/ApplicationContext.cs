using ContextStudier.Core.Entitites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContextStudier.Infrastructure.DataAccess
{
    internal class ApplicationContext : IdentityDbContext<User>
    {
        internal DbSet<User> Users { get; set; }

        internal DbSet<Folder> Folders { get; set; }

        internal DbSet<Card> Cards { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}