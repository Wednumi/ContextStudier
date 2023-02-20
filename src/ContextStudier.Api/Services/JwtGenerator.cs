using ContextStudier.Core.Entitites;
using ContextStudier.Core.Interfaces.Security;
using ContextStudier.Presentation.Core.AccountModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ContextStudier.Api.Services
{
    public class JwtGenerator
    {
        private readonly ISecurityKeySource _keySource;

        public JwtGenerator(ISecurityKeySource keySource)
        {
            _keySource = keySource;
        }

        public AuthenticatedUserModel Generate(User user, IList<string> roleNames)
        {
            var claims = GetUserClaims(user);
            AddRoleClaims(claims, roleNames);

            var header = new JwtHeader(
                new SigningCredentials(
                    new SymmetricSecurityKey(_keySource.GetKeyBytes()),
                    SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityToken(header, new JwtPayload(claims));
            var output = new AuthenticatedUserModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
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
            foreach (var roleName in roleNames)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }
        }
    }
}