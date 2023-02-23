using Microsoft.Extensions.DependencyInjection;
using ContextStudier.Infrastructure;
using ContextStudier.Core;
using Microsoft.Extensions.Configuration;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using ContextStudier.Core.Entitites;

namespace ContextStudier.IntegrationTests
{
    public class ConfiguredTest
    {
        private IServiceProvider _serviceProvider;

        public ConfiguredTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCoreServices();

            string path = Path.GetFullPath(@"config.json");
            var config = new ConfigurationBuilder().AddJsonFile(path).Build();
            serviceCollection.AddTransient<IConfiguration>(provider => config);
            serviceCollection.AddInfrastructureServices(config);

            var loggerMock = new Mock<ILogger<UserManager<User>>>();
            serviceCollection.AddTransient<ILogger<UserManager<User>>>(provider => loggerMock.Object);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        protected T GetService<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
