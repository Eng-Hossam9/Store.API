using AutoMapper;
using Demo.API.Errors;
using Demo.API.Extensions;
using Demo.Core.DTO.Accounts;
using Demo.Core.Models.Identity;
using Demo.Core.ServicesInterFaces;
using Demo.Service.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _UserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(IUserService userService, UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
        {
            _UserService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _UserService.LogInAsync(model);
            if (user == null) return Unauthorized(new ApiErrorResponse(401));
            return Ok(user);  
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _UserService.RegisterAsync(model);
            if (user == null) return BadRequest(new ApiErrorResponse(400));
            return Ok(user);
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var CurrentUser = User.FindFirstValue("Email");
            if (CurrentUser is null) return BadRequest(new ApiErrorResponse(400));

            var user = await _userManager.FindByEmailAsync(CurrentUser);
            if (user is null) return BadRequest(new ApiErrorResponse(400));

            return Ok(new UserDTO()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });

        }

        
        [Authorize]
        [HttpGet("address")]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var user = await _userManager.FindUserAddress(User);
            if (user is null) return BadRequest(new ApiErrorResponse(400));
            return Ok(_mapper.Map<UserAddressDTO>(user.Address));
        }





        [Authorize]
        [HttpPut("address")]
        public async Task<IActionResult> UpdateCurrentUserAddress([FromBody] UserAddressDTO updateUserAddressDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var CurrentUser =  await _userManager.FindUserAddress(User);

            if (CurrentUser is null) return BadRequest(new ApiErrorResponse(400));

            CurrentUser.Address = _mapper.Map<Address>(updateUserAddressDTO);


            var result = await _userManager.UpdateAsync(CurrentUser);
            if (!result.Succeeded)
                return BadRequest(new ApiErrorResponse(400, "Failed to update the address"));

            return Ok(new ApiErrorResponse(200, "Address updated successfully"));
        }
    }
}
