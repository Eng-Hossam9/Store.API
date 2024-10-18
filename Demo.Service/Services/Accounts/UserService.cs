using Demo.Core.DTO.Accounts;
using Demo.Core.Models.Identity;
using Demo.Core.ServicesInterFaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Service.Services.Accounts
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<UserDTO> LogInAsync(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return null;
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return null;
            return new UserDTO()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };

        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO model)
        {
            var Existuser = await _userManager.FindByEmailAsync(model.Email);

            if (Existuser is not null) return null;

            var user = new AppUser()
            {
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                DisplayName = model.DisplayName,
                UserName = model.Email.Split("@")[0]
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return null;

            return new UserDTO()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
        }
    }
}
