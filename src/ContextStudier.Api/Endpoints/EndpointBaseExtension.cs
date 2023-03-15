using Ardalis.ApiEndpoints;
using System.Security.Claims;

namespace ContextStudier.Api.Endpoints
{
    public static class EndpointBaseExtension
    {
        public static string GetUserId(this EndpointBase endpointBase)
        {
            return endpointBase.User.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .First().Value;
        }
    }
}
