using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace BlazorNitflex.Services
{
    public class CustomAuthenticationStateProvider :AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;

        private const string JwtKey = "JwtToken";

        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //Get jwt token from session
            string? jwt = await _sessionStorageService.GetItemAsStringAsync(JwtKey);


            if (string.IsNullOrEmpty(jwt))
            {
                // Return unauthenticated state
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            //Parse claims from jwt
            IEnumerable<Claim> claims = ParseClaimsFromJwt(jwt);

            ClaimsIdentity identity = new ClaimsIdentity(claims,"jwt");

            var user = new ClaimsPrincipal(identity);


            return new AuthenticationState(user);
        }

        public void NotifyUserLogin(string jwt)
        {
            //Notify blazor about login
            var claims = ParseClaimsFromJwt(jwt);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void NotifyUserLogout()
        {
            // Notify blazor about logout
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        //Parse claims from jwt
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt) 
        {
            var jwtPayload = jwt.Split('.')[1];

            var jwtJsonBytes = AddPadding(jwtPayload);

            var jwtKeyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jwtJsonBytes);


            List<Claim> claims = new List<Claim>();
            if (jwtKeyValuePairs is null)
            {
                return claims;
            }

            foreach (var KVP in jwtKeyValuePairs)
            {
                claims.Add(new Claim(KVP.Key,KVP.Value.ToString()));
            }

            return claims;
        }

        //Add padding to parsed base64 string
        private string AddPadding(string base64string)
        {
            switch (base64string.Length % 4)
            {
                case 2: return base64string + "==";
                case 3: return base64string + "=";
                default: return base64string;
            }
        }
    }
}
