using Demo.API.Errors;
using Demo.Core.DTO.Accounts;
using Demo.Core.Models.Identity;
using Demo.Core.ServicesInterFaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _UserService;

        public AccountsController(IUserService userService)
        {
            _UserService = userService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LoginDTO model)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var user =await _UserService.LogInAsync(model);
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

    }
}
