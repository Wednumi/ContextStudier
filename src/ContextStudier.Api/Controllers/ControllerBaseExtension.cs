using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContextStudier.Api.Controllers
{
    public static class ControllerBaseExtension
    {
        public static string GetUserId(this ControllerBase controllerBase)
        {
            return controllerBase.User.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .First().Value;
        }
    }
}
