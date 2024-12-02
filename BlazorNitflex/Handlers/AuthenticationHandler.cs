using BlazorNitflex.Services.Interfaces;
using System.Net.Http.Headers;

namespace BlazorNitflex.Handlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwt = await _authenticationService.GetJwtAsync();

            if (string.IsNullOrEmpty(jwt) is false)
            {
                //Add jwt to authorization header in http requests
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
