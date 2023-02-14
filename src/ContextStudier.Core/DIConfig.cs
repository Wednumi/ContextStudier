using ContextStudier.Core.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ContextStudier.Core
{
    public static class DIConfig
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<UserManager<User>>();
        }
    }
}
