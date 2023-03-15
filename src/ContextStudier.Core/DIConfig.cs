using ContextStudier.Core.Entitites;
using ContextStudier.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ContextStudier.Core
{
    public static class DIConfig
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<FolderService>();
        }
    }
}
