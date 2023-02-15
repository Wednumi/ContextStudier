namespace ContextStudier.Core.Interfaces.Security
{
    public interface ISecurityKeySource
    {
        public Task<byte[]> GetKeyBytes();
    }
}
