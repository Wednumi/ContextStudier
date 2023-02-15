using ContextStudier.Core.Interfaces.Security;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace ContextStudier.Infrastructure.Security
{
    internal class ConfigSecurityKeySource : ISecurityKeySource
    {
        private readonly IConfiguration _config;

        public ConfigSecurityKeySource(IConfiguration config)
        {
            _config = config;
        }

        public async Task<byte[]> GetKeyBytes()
        {
            return Encoding.UTF8.GetBytes(_config["SecurityKey"]!);
        }
    }
}
