using Demo.API.Errors;
using Demo.Core.ServicesInterFaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {

        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("{basketId}")]
        [Authorize]
        public async Task<IActionResult> CreatePaymentIntent(string basketId)
        {
            if (basketId is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var basket = await _paymentService.CreateOrUpdatePaymentIntentId(basketId);

            if (basket is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
             
            return Ok(basket);
        }
    }
}
