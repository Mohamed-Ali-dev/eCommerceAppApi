using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace eCommerceApp.Infrastructure.Repositories.Authentication
{
    public class RoleManagement(UserManager<AppUser> userManager) : IRoleManagement
    {
        public async Task<bool> AddUserToRole(AppUser user, string roleName) =>
            (await userManager.AddToRoleAsync(user, roleName)).Succeeded;

        public async Task<List<string>> GetUserRoles(string userEmail)
        {
            var user =await userManager.FindByEmailAsync(userEmail);
            var roles = await userManager.GetRolesAsync(user!);
            return roles.ToList();
        }
    }
}
