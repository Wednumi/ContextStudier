using ContextStudier.Api.Models.AccountModels;
using ContextStudier.Core.Entitites;
using Microsoft.AspNetCore.Identity;

namespace ContextStudier.Api.Endpoints.Tokens
{
    public class CredentialMatcher
    {
        private readonly UserManager<User> _userManager;

        public CredentialMatcher(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        internal async Task<User> GetValidUserAsync(CredentialModel credential)
        {
            var user = await _userManager.FindByNameAsync(credential.UserName)
                ?? throw new UserNotFoundException();

            var valid = await _userManager.CheckPasswordAsync(user, credential.Password);

            return valid 
                ? user
                : throw new WrongPasswordException();
        }
    }
}
