using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ContextStudier.Presentation.BlazorWASM.Security
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private static AuthenticationState s_anonymous
            = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        private readonly ILocalStorageService _localStorage;

        private readonly HttpClient _client;

        public AuthStateProvider(ILocalStorageService localStorage, HttpClient client)
        {
            _localStorage = localStorage;
            _client = client;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(token))
            {
                return s_anonymous;
            }

            AddAuthHeader(token);

            return ConstructAuthenticationState(token);
        }

        public void NotifyUserAuthentication(string token)
        {
            AddAuthHeader(token);
            var state = ConstructAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }

        private void AddAuthHeader(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        private AuthenticationState ConstructAuthenticationState(string token)
        {
            var claims = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwtAuthType"));
            return new AuthenticationState(claimsPrincipal);
        }

        public void NotifyLogout()
        {
            NotifyAuthenticationStateChanged(Task.FromResult(s_anonymous));
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
