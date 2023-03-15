using ContextStudier.Core.Entitites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContextStudier.Infrastructure.DataAccess
{
    public class ApplicationContext : IdentityDbContext<User>
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