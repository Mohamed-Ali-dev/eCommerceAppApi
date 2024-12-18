using eCommerceApp.Domain.Models.Identity;
using System.Security.Claims;

namespace eCommerceApp.Domain.Interfaces.Authentication
{
    public interface ITokenManagement
    {
        RefreshToken GetRefreshToken();
        List<Claim> GetUserClaimsFromToken(string token);
        Task<bool> ValidateRefreshToken(string refreshToken); 
        Task<string> GetUserIdByRefreshToken(string refreshToken);
        Task<int> AddRefreshToken(string userId, RefreshToken refreshToken);
        Task<int> UpdateRefreshToken(string userId, string refreshToken);
        string GenerateToken(List<Claim> claims);
    }
}
