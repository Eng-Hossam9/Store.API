using Demo.Core.DTO.Accounts;
using Demo.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Demo.API.Extensions
{
    public static class  UserMangerExtension
    {
        public static async Task<AppUser> FindUserAddress(this UserManager<AppUser> _userManager, ClaimsPrincipal User)
        {
            var UserEmail = User.FindFirstValue("Email");
            if (UserEmail is null) return null;

            var user =await _userManager.Users.Include(e=>e.Address).FirstOrDefaultAsync(e=>e.Email==UserEmail);
            if (user is null) return null;
            return user;

        }
    }
}
