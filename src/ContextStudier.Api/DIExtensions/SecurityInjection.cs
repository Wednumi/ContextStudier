using ContextStudier.Core.Interfaces.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ContextStudier.Api.DIExtensions
{
    internal static class SecurityInjection
    {
        internal static void AddSetSecurity(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var tokenSource = serviceProvider
                .GetRequiredService<ISecurityKeySource>();
            var key = tokenSource.GetKeyBytes();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                });
        }
    }
}
