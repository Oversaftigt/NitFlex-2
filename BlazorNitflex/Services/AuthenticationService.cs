using Blazored.SessionStorage;
using BlazorNitflex.Models;
using BlazorNitflex.Services.Interfaces;
using System.Net.Http.Json;

namespace BlazorNitflex.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string JwtKey = "JwtToken";
        private string? _jwtCache;

        private ISessionStorageService _sessionStorageService;
        private IHttpClientFactory _httpClientFactory;

        public AuthenticationService(ISessionStorageService sessionStorageService, IHttpClientFactory httpClientFactory)
        {
            this._sessionStorageService = sessionStorageService;
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetJwtAsync()
        {
            if (string.IsNullOrEmpty(_jwtCache))
            {
                _jwtCache = await _sessionStorageService.GetItemAsync<string>(JwtKey);
            }
            return _jwtCache;
        }

        public async Task SetJwtAsync(string jwt)
        {
            await _sessionStorageService.SetItemAsync(JwtKey, jwt);

        }

        public async Task<bool> Register(RegisterModel regsiterModel)
        {
            var response = await _httpClientFactory.CreateClient("unauthenticatedIdentityclient")
                                 .PostAsync("api/account/register", JsonContent.Create(regsiterModel));

            if (response.IsSuccessStatusCode is true)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> LoginAsync(LoginModel loginModel)
        {
            var response = await _httpClientFactory.CreateClient("unauthenticatedIdentityclient")
                                 .PostAsync("api/Account/login", JsonContent.Create(loginModel));

            if (response.IsSuccessStatusCode is true)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseModel>();

                await SetJwtAsync(result.Token);

                return true;
            }
            return false;
        }

        public async Task LogoutAsync()
        {
            await _sessionStorageService.RemoveItemAsync(JwtKey);

            _jwtCache = null;
        }
        public async Task<string> GetUserIdAsync()
        {
            var httpclient = _httpClientFactory.CreateClient("identityclient");

            var request = new HttpRequestMessage(HttpMethod.Get, "api/account/userId");

            var response = await httpclient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var userId = await response.Content.ReadAsStringAsync();
            return userId;
        }

        public async Task<string> GetEmailAsync()
        {
            var httpclient = _httpClientFactory.CreateClient("identityclient");

            var request = new HttpRequestMessage(HttpMethod.Get, "api/account/email");

            var response = await httpclient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var email = await response.Content.ReadAsStringAsync();
            return email;
        }

       
    }
}
