using ContextStudier.Core.Entitites;
using ContextStudier.Core.Interfaces.DataAccess;

namespace ContextStudier.IntegrationTests
{
    public class DataAccessTests : ConfiguredTest
    {
        [Fact]
        public void Repository_has_access_to_database()
        {
            //Arrane
            var sut = GetService<IRepositoryFactory>()
                .GetRepository<User>();

            //Act
            var result = sut.ListAsync().Result;

            //Assert
            Assert.NotNull(result);
        }
    }
}
