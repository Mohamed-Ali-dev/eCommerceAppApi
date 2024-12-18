using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Domain.Models.Identity;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eCommerceApp.Infrastructure.Repositories.Authentication
{
    public class TokenManagement(AppDbContext context, IOptions<JwtOptions> jwtOptions) : ITokenManagement
    {
        public RefreshToken GetRefreshToken()
        {
            const int byteSie = 64;
                byte[] randomBytes = new byte[byteSie];
            using(RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            string token = Convert.ToBase64String(randomBytes);

            return new RefreshToken
            {
                Token = WebUtility.UrlEncode(token),
                ExpiredOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow
            };
            
        }

        public List<Claim> GetUserClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken != null)
                return jwtToken.Claims.ToList();
            else
                return [];
        }

        public async Task<string> GetUserIdByRefreshToken(string refreshToken) =>
             (await context.refreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken))!.UserId;
        public string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SigningKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(jwtOptions.Value.LifeTime);
            var token = new JwtSecurityToken(
                issuer: jwtOptions.Value.Issuer, 
                audience: jwtOptions.Value.Audience,
                claims:claims, 
                expires: expiration, 
                signingCredentials: cred);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<int> AddRefreshToken(string userId, RefreshToken refreshToken)
        {
            var newRefreshToken = new RefreshToken
            {
                Id = refreshToken.Id,
                UserId = userId,
                Token = refreshToken.Token,
                CreatedOn = refreshToken.CreatedOn,
                ExpiredOn = refreshToken.ExpiredOn,
            };
            context.refreshTokens.Add(newRefreshToken);
            return await context.SaveChangesAsync();
        }
        public async Task<int> UpdateRefreshToken(string userId, string newRefreshToken)
        {
            // Find the refresh token associated with the given userId
            var refreshTokenEntity = await context.refreshTokens
                .FirstOrDefaultAsync(rt => rt.UserId == userId);

            // Check if the entity exists
            if (refreshTokenEntity == null)
                return -1; 

            // Update the token
            refreshTokenEntity.Token = newRefreshToken;
            refreshTokenEntity.ExpiredOn = DateTime.UtcNow.AddDays(10);
            refreshTokenEntity.CreatedOn = DateTime.UtcNow;

            // Save changes to the database
            return await context.SaveChangesAsync();
        }

        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            var refreshTokenDb = await context.refreshTokens.FirstOrDefaultAsync(x =>x.Token == refreshToken);
            if(refreshTokenDb != null && refreshTokenDb.IsActive)
            {
                return true;
            }
           return false;
        }
    }
}
