using ContextStudier.Core.Entitites;
using ContextStudier.Core.Interfaces.DataAccess;
using ContextStudier.Core.Interfaces.Security;
using ContextStudier.Infrastructure.DataAccess;
using ContextStudier.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ContextStudier.Infrastructure
{
    public static class DIConfig
    {
        public static void AddInfrastructureServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddTransient<IDesignTimeDbContextFactory<ApplicationContext>,
                ApplicationContextFactory>();
            services.AddTransient<ApplicationContext>(provider =>
            {
                var factory = provider
                    .GetRequiredService<IDesignTimeDbContextFactory<ApplicationContext>>();
                return factory.CreateDbContext(Array.Empty<string>());
            });

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