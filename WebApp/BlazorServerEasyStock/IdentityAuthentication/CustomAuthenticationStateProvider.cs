using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;
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


        public async  Task<AuthenticationState> GetAuthentication2StateAsync()
        {
            string authToken = await GetAccessTokenAsync();

            var claimsPrincipal = new ClaimsIdentity();
            httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(authToken))
            {
                try
                {
                    claimsPrincipal = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", authToken.Replace("\"", ""));
                }
                catch
                {
                    await ProtectedLocalStore.DeleteAsync("Authentication");
                    claimsPrincipal = new ClaimsIdentity();
                }
            }

            var user = new ClaimsPrincipal(claimsPrincipal);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
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

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer
                .Deserialize<Dictionary<string, object>>(jsonBytes);

            var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
