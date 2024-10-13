using Demo.API.Errors;
using Demo.Repository.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }


        [HttpGet("notfound")]
        public async Task<IActionResult> GetNotFoundRequestError()
        {
            var product = await _context.Products.FindAsync(100);
            if (product is null) return NotFound(new ApiErrorResponse(404));

            return Ok();
        }


        [HttpGet("servererror")]
        public async Task<IActionResult> GetServerError()
        {
            var product = await _context.Products.FindAsync(100);

            product.ToString();

            return Ok();
        }


        [HttpGet("badrequest")]
        public async Task<IActionResult> GetBadRequestError()
        {
            var product = await _context.Products.FindAsync(100);

            return BadRequest(new ApiErrorResponse(400));
        }



        [HttpGet("badrequest/{id}")]
        public async Task<IActionResult> GetBadRequestError(int id)
        {
            var product = await _context.Products.FindAsync(100);

            return Ok();
        }


        [HttpGet("unauthorized")]
        public async Task<IActionResult> GetUnauthorizedError()
        {
            var product = await _context.Products.FindAsync(100);

            return Unauthorized(new ApiErrorResponse(401));
        }

    }
}
 