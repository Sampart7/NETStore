using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.UsersAdmin
{
    public class CreateUser
    {
        private UserManager<IdentityUser> _userManager;

        public CreateUser(UserManager<IdentityUser> userManager)
        {   
            _userManager = userManager;
        }

        public class Request
        {
            public string UserName { get; set; }
        }
        public async Task<bool> Do(Request request)
        {
            var adminUser = new IdentityUser()
            {
                UserName = request.UserName,
            };

            await _userManager.CreateAsync(adminUser, "password");

            var adminClaim = new Claim("Role", "Admin");

            await _userManager.AddClaimAsync(adminUser, adminClaim);

            return true;
        }
    }
}
