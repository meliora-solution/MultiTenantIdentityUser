using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using System.Text.Json;

namespace BlazorServerEasyStock.IdentityAuthentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        private readonly ProtectedLocalStorage ProtectedLocalStore;
        private readonly HttpClient httpClient;

        public CustomAuthenticationStateProvider(ProtectedLocalStorage protectedLocalStore, HttpClient httpClient)
        {
            this.ProtectedLocalStore = protectedLocalStore;
            this.httpClient = httpClient;
        }
        
        public async Task<DateTime?> GetExpirationDateAsync()
        {
            try
            {
                var authenticationModel = await ProtectedLocalStore.GetAsync<string>("Authentication");
                if (authenticationModel.Value == null)
                {
                    return null; // No authentication model found
                }
                return Deserialize(authenticationModel.Value).ExpirationDate;
            }
            catch
            {
                return null; // Error occurred while getting the token
            }
        }
        public async Task<string?> GetAccessTokenAsync()
        {
            try
            {
                var authenticationModel = await ProtectedLocalStore.GetAsync<string>("Authentication");
                if (authenticationModel.Value == null)
                {
                    return null; // No authentication model found
                }
                return Deserialize(authenticationModel.Value).AccessToken;
            }
            catch
            {
                return null; // Error occurred while getting the token
            }
        }

        public async Task<string?> GetRefreshTokenAsync()
        {
            try
            {
                var authenticationModel = await ProtectedLocalStore.GetAsync<string>("Authentication");
                if (authenticationModel.Value == null)
                {
                    return null; // No authentication model found
                }
                return Deserialize(authenticationModel.Value).RefreshToken;
            }
            catch
            {
                return null; // Error occurred while getting the token
            }
        }
        public async Task Logout()
        {
            // Clear client-side authentication information
            // Example: Remove cookie, clear local storage, etc.
            // Here, assuming you set a cookie named "Authentication", you can clear it
            // Also, clear any other client-side information related to authentication
            // ...

            // Notify that the authentication state has changed
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var authenticationModel = await ProtectedLocalStore.GetAsync<string>("Authentication");
                if (authenticationModel.Value == null)
                {
                    return await Task.FromResult(new AuthenticationState(anonymous));
                }

                return await Task.FromResult(new AuthenticationState(SetClaims(Deserialize(authenticationModel.Value).Username!)));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }
        }

        public async Task UpdateAuthenticationState(AuthenticationModel authenticationModel)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = new();
                if (authenticationModel is not null)
                {
                    claimsPrincipal = SetClaims(authenticationModel.Username!);
                    await ProtectedLocalStore.SetAsync("Authentication", Serialize(authenticationModel));
                }
                else
                {
                    await ProtectedLocalStore.DeleteAsync("Authentication");
                    claimsPrincipal = anonymous;
                }

                // NotifyAuthenticationStateChanged remains the same, as it will trigger a re-render in Blazor
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            }
            catch
            {
                await Task.FromResult(new AuthenticationState(anonymous));
            }
        }

        private ClaimsPrincipal SetClaims(string email) => new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
    {
        new Claim(ClaimTypes.Name, email)
    }, "CustomAuth"));

        private AuthenticationModel Deserialize(string serializeString) => JsonSerializer.Deserialize<AuthenticationModel>(serializeString)!;

        private string Serialize(AuthenticationModel model) => JsonSerializer.Serialize(model);
    }
}
