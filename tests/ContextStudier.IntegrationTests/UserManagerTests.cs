using ContextStudier.Core.Entitites;
using Microsoft.AspNetCore.Identity;

namespace ContextStudier.IntegrationTests
{
    public class UserManagerTests : ConfiguredTest
    {
        [Fact]
        public void UserManager_creates_user()
        {
            var sut = GetService<UserManager<User>>();
        }
    }
}
