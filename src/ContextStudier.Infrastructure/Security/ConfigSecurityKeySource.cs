using ContextStudier.Core.Interfaces.Security;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace ContextStudier.Infrastructure.Security
{
    internal class ConfigSecurityKeySource : ISecurityKeySource
    {
        private byte[]? _keyBytes = null;

        private readonly IConfiguration _config;

        public ConfigSecurityKeySource(IConfiguration config)
        {
            _config = config;
        }

        public byte[] GetKeyBytes()
        {
            if(_keyBytes is not null)
            {
                return _keyBytes;
            }
            _keyBytes = Encoding.UTF8.GetBytes(_config["SecurityKey"]!);
            return _keyBytes;
        }
    }
}
