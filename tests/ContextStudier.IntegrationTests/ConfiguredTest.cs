using Microsoft.Extensions.DependencyInjection;
using ContextStudier.Infrastructure;
using ContextStudier.Core;
using Microsoft.Extensions.Configuration;
using ContextStudier.Infrastructure.DataAccess;

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

            _serviceProvider = serviceCollection.BuildServiceProvider();

            ResetDatabase();
        }

        private void ResetDatabase()
        {
            var context = GetService<ApplicationContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        protected T GetService<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
