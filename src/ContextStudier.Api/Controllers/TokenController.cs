using ContextStudier.Api.Models.AccountModels;
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

        private readonly ISecurityKeySource _keySource;

        public TokenController(UserManager<User> userManager, ISecurityKeySource keySource)
        {
            _userManager = userManager;
            _keySource = keySource;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]CredentialModel credential)
        {
            if(ModelState.IsValid is false)
            {
                return BadRequest();
            }

            var validUser = await ValidUser(credential);
            if (validUser is null)
            {
                return BadRequest();
            }

            return Ok(await GenerateToken(validUser));
        }

        private async Task<User?> ValidUser(CredentialModel credential)
        {
            var user = await _userManager.FindByNameAsync(credential.UserName);
            var valid = await _userManager.CheckPasswordAsync(user, credential.Password);
            return valid ? user : null;
        }

        private async Task<dynamic> GenerateToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = GetUserClaims(user);
            AddRoleClaims(claims, roles);

            var header = new JwtHeader(
                new SigningCredentials(
                    new SymmetricSecurityKey(await _keySource.GetKeyBytes()),
                    SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityToken(header, new JwtPayload(claims));
            var output = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = user.UserName,
            };
            return output;
        }

        private List<Claim> GetUserClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf,
                    new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,
                    new DateTimeOffset(DateTime.Now.AddDays(30)).ToUnixTimeSeconds().ToString()),
            };
            return claims;
        }

        private void AddRoleClaims(IList<Claim> claims, IList<string> roleNames)
        {
            foreach(var roleName in roleNames)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }
        }
    }
}
