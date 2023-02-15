using ContextStudier.Core.Entitites;
using ContextStudier.Core.Interfaces.DataAccess;
using ContextStudier.Core.Interfaces.Security;
using ContextStudier.Infrastructure.DataAccess;
using ContextStudier.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
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
            services.AddSingleton<ISecurityKeySource, ConfigSecurityKeySource>();
        }
    }
}