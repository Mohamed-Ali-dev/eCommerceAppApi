using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Domain.Models.Identity;
using eCommerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eCommerceApp.Infrastructure.Repositories.Authentication
{
    public class UserManagement(IRoleManagement roleManagement,
        UserManager<AppUser> userManager, AppDbContext context) : IUserManagement
    {
        public async Task<IEnumerable<AppUser>?> GetAllUsers() =>
           await context.Users.ToListAsync();
        public async Task<AppUser?> GetUserByEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user!;
        }
        public async Task<AppUser> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return user!;
        }

        public async Task<bool> CreateUser(AppUser appUser)
        {
            var _user = await GetUserByEmail(appUser.Email!);
            if (_user != null) return false;

            return (await userManager.CreateAsync(appUser!, appUser!.PasswordHash!)).Succeeded;
        }
        public async Task<bool> LoginUser(AppUser appUser)
        {
            var user = await GetUserByEmail(appUser.Email!);

            if (user is null || !await userManager.CheckPasswordAsync(user, appUser.PasswordHash!))
                return false;

            var roleName = await roleManagement.GetUserRoles(appUser.Email!)!;
            if (roleName == null) return false;

            return true;

        }

        public async Task<int> RemoveUserByEmail(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            context.Users.Remove(user);
            return await context.SaveChangesAsync();
        }
        public async Task<List<Claim>> GetUserClaims(string email)
        {
            var user = await GetUserByEmail(email);

            var roleName = await roleManagement.GetUserRoles(user!.Email!);

            List<Claim> claims = new List<Claim>
            {
                new Claim("FullName", user!.FullName),
                new Claim(ClaimTypes.NameIdentifier, user!.Id),
                new Claim(ClaimTypes.Email, user!.Email!)
            };
            foreach (var role in roleName)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
