using ContextStudier.Api.Services;
using ContextStudier.Core.Entitites;
using ContextStudier.Core.Interfaces.Security;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Text;

namespace ContextStudier.ApiUnitTests
{
    public class JwtGeneratorTests
    {
        [Fact]
        public void Does_generate_any_token()
        {
            var keySourceMock = new Mock<ISecurityKeySource>();
            keySourceMock.Setup(s => s.GetKeyBytes())
                .Returns(new byte[32]);
            var user = new User { UserName = "name" };
            var sut = new JwtGenerator( keySourceMock.Object);
            var roles = new List<string>();

            var token = sut.Generate(user, roles);

            Assert.NotNull(token);
        }

        [Fact]
        public void Token_includes_roles()
        {
            var keySourceMock = new Mock<ISecurityKeySource>();
            keySourceMock.Setup(s => s.GetKeyBytes())
                .Returns(Encoding.UTF8.GetBytes("test-test-test-test"));
            var user = new User { UserName = "name" };
            var sut = new JwtGenerator(keySourceMock.Object);
            var emptyRoles = new List<string>();
            var filledRoles = new List<string>() { "role1" };

            var tokenWithoutRoles = sut.Generate(user, emptyRoles);
            var tokenWithRoles = sut.Generate(user, filledRoles);

            Assert.NotEqual(tokenWithoutRoles, tokenWithRoles);
        }
    }
}
