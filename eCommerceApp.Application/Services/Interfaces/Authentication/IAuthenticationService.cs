using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Identity;

namespace eCommerceApp.Application.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthResponse> CreateUser(CreateUser user);
        Task<AuthResponse> LoginUser(LogInUser user, string? inputRefreshToken);
        Task<AuthResponse> ReviveToken(string refreshToken);
    }
}
