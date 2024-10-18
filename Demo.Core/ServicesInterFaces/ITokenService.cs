using Demo.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.ServicesInterFaces
{
    public interface ITokenService
    {
       Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> _userManager);
    }
}
