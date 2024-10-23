using AutoMapper;
using Demo.API.Errors;
using Demo.Core.DTO.Orders;
using Demo.Core.Models;
using Demo.Core.RepositoriesInterFaces;
using Demo.Core.ServicesInterFaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IBasketRepository basketRepository, IMapper mapper)
        {
            _orderService = orderService;
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDTO Model)
        {
            var UserEmail = User.FindFirstValue("Email");
            if (UserEmail is null) return Unauthorized(new ApiErrorResponse(401));
            var AddressShipping = _mapper.Map<Address>(Model.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(UserEmail, Model.BasketId, Model.DeliveryMethodId, AddressShipping);
            if (order is null) return BadRequest(new ApiErrorResponse(400));
            return Ok(_mapper.Map<OrderResponseDTO>(order));

        }

    }
}
