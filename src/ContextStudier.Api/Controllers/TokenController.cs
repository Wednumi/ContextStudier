using ContextStudier.Api.Models.AccountModels;
using ContextStudier.Api.Services;
using ContextStudier.Core.Entitites;
using ContextStudier.Core.Interfaces.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ContextStudier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly JwtGenerator _jwtGenerator;

        public TokenController(UserManager<User> userManager, JwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CredentialModel credential)
        {
            if(ModelState.IsValid is false)
            {
                return BadRequest(ModelState);
            }

            var validUser = await ValidUser(credential);
            if (validUser is null)
            {
                return BadRequest("Wrong login or password");
            }

            var roleNames = await _userManager.GetRolesAsync(validUser);
            return Ok(_jwtGenerator.Generate(validUser, roleNames));
        }

        private async Task<User?> ValidUser(CredentialModel credential)
        {
            var user = await _userManager.FindByNameAsync(credential.UserName);
            var valid = await _userManager.CheckPasswordAsync(user, credential.Password);
            return valid ? user : null;
        }
    }
}
