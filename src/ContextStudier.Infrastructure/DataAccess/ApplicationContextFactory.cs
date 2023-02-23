using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ContextStudier.Infrastructure.DataAccess
{
    internal class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        private readonly IConfiguration? _configuration;

        public ApplicationContextFactory()
        { }

        public ApplicationContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ApplicationContext CreateDbContext(string[] args)
        {
            if (_configuration is not null)
            {
                return CreateWithConnectionString(_configuration.GetConnectionString("Default")!);
            }
            else
            {
                var connectionString = ConstructConnectionString(args);
                return CreateWithConnectionString(connectionString);
            }
        }

        private ApplicationContext CreateWithConnectionString(string connectionString)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionBuilder.UseSqlServer(connectionString);
            return new ApplicationContext(optionBuilder.Options);
        }

        private string ConstructConnectionString(string[] args)
        {
            return args.Aggregate((current, next) => current + " " + next).Trim();
        }
    }
}