using eCommerceApp.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Domain.Interfaces.Authentication
{
    public interface IRoleManagement
    {
        Task<List<string>> GetUserRoles(string uerEmail);
        Task<bool> AddUserToRole(AppUser user, string roleName);

    }
}
