using BlazorNitflex.Models;

namespace BlazorNitflex.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> GetEmailAsync();
        ValueTask<string> GetJwtAsync();
        Task<string> GetUserIdAsync();
        Task LoginAsync(LoginModel loginModel);
        Task LogoutAsync();
    }
}