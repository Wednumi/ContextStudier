using Blazored.LocalStorage;
using ContextStudier.Api.Models.AccountModels;
using ContextStudier.Presentation.Core.AccountModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ContextStudier.Presentation.BlazorWASM.Security
{
    public class AuthenticationService
    {
        private readonly HttpClient _client;

        private readonly AuthStateProvider _authStateProvider;

        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client,
            AuthStateProvider authStateProvider,
            ILocalStorageService localStorage)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<AuthenticationResultModel> LoginAsync(CredentialModel credentials)
        {
            var authResult = await _client.PostAsJsonAsync("Token", credentials);

            if (authResult.IsSuccessStatusCode is false)
            {
                return new AuthenticationResultModel(false, await authResult.Content.ReadAsStringAsync());
            }

            var result = await authResult.Content.ReadFromJsonAsync<AuthenticatedUserModel>();
            await _localStorage.SetItemAsync("authToken", result!.AccessToken);

            _authStateProvider.NotifyUserAuthentication(result.AccessToken);

            return new AuthenticationResultModel(true);
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _authStateProvider.NotifyLogout();
        }
    }
}
