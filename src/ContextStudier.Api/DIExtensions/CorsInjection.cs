namespace ContextStudier.Api.DIExtensions
{
    internal static class CorsInjection
    {
        internal static void AddSetCors(this IServiceCollection services, string allowedOrigins,
            IConfiguration config)
        {
            services.AddCors(o =>
            {
                o.AddPolicy(allowedOrigins,
                    policy =>
                    {
                        policy.WithOrigins(config["AllowedConsumer"]!)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
    }
}
