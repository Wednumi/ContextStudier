using ContextStudier.Api.Endpoints.Tokens;
using ContextStudier.Core.Entitites;
using ContextStudier.Core.Interfaces.Security;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ContextStudier.ApiUnitTests
{
    public class JwtGeneratorTests
    {
        private readonly Mock<ISecurityKeySource> _keySourceMock;

        private readonly Mock<UserManager<User>> _userManagerMock;

        private readonly JwtGenerator _sut;

        public JwtGeneratorTests()
        {
            _keySourceMock = new Mock<ISecurityKeySource>();
            _keySourceMock.Setup(s => s.GetKeyBytes())
                .Returns(new byte[32]);

            var storeMock = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(storeMock.Object, null, 
                null, null, null, null, null, null, null);
            _sut = new JwtGenerator(_keySourceMock.Object, _userManagerMock.Object);
        }

        [Fact]
        public void GenerateAsync_IncludeClaims()
        {
            //Arrange
            var user = new User { UserName = "name" };
            _userManagerMock.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(new List<string>());

            //Act
            var result = _sut.GenerateAsync(user).Result;

            //Assert
            var tokenHandler = new JwtSecurityTokenHandler();
            var userName = tokenHandler.ReadJwtToken(result.AccessToken).Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            Assert.Equal(user.UserName, userName);
        }

        [Fact]
        public void Generate_IncludesRoles()
        {
            //Arrange
            var user = new User { UserName = "name" };
            var roles = new List<string>() { "role1", "role2" };
            _userManagerMock.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(roles);

            //Act
            var result = _sut.GenerateAsync(user).Result;

            //Assert
            var tokenHandler = new JwtSecurityTokenHandler();
            var storedRoles = tokenHandler.ReadJwtToken(result.AccessToken).Claims
                .Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            Assert.Equal(roles, storedRoles);
        }
    }
}
