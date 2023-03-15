using ContextStudier.Api.Endpoints.Tokens;

namespace ContextStudier.Api.DIExtensions
{
    internal static class ApiServicesInjection
    {
        internal static void AddApiServices(this IServiceCollection services)
        {
            services.AddTransient<JwtGenerator>();
            services.AddTransient<CredentialMatcher>();
        }
    }
}
