using ContextStudier.Api.Services;

namespace ContextStudier.Api.DIExtensions
{
    internal static class ApiServicesInjection
    {
        internal static void AddApiServices(this IServiceCollection services)
        {
            services.AddTransient<JwtGenerator>();
        }
    }
}
