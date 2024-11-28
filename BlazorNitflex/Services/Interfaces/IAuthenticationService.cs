using BlazorNitflex.Models;

namespace BlazorNitflex.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> GetEmailAsync();
        Task<string> GetJwtAsync();
        Task<string> GetUserIdAsync();
        Task SetJwtAsync(string jwt);
        Task<bool> LoginAsync(LoginModel loginModel);
        Task LogoutAsync();
    }
}