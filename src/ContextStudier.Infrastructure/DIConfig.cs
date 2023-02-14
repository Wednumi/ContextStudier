using ContextStudier.Core.Entitites;
using ContextStudier.Core.Interfaces.DataAccess;
using ContextStudier.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContextStudier.Infrastructure
{
    public static class DIConfig
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(ServiceLifetime.Transient);
            services.AddTransient<IRepositoryFactory, RepositoryFactory>();

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);

                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationContext>();
        }
    }
}
