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
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderService orderService, IBasketRepository basketRepository, IMapper mapper,IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _basketRepository = basketRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrderForSpacificUser()
        {
            var UserEmail = User.FindFirstValue("Email");
            if (UserEmail is null) return Unauthorized(new ApiErrorResponse(401));
            var order = await _orderService.GetOrdersForSpecificUserAsync(UserEmail);
            if (order is null) return NotFound(new ApiErrorResponse(404));
            return Ok(_mapper.Map<IEnumerable<OrderResponseDTO>>(order));
        }

        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var UserEmail = User.FindFirstValue("Email");
            if (UserEmail is null) return Unauthorized(new ApiErrorResponse(401));
            var order = await _orderService.GetOrderByIdAsync(UserEmail,orderId);
            if (order is null) return NotFound(new ApiErrorResponse(404));
            return Ok(_mapper.Map<OrderResponseDTO>(order));

        }

        [HttpGet("DeliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var Delivery= await _unitOfWork.CreateRepository<DeliveryMethod, int>().GetAllAsync();
            if (Delivery is null) return BadRequest(new ApiErrorResponse(400));
            return Ok(Delivery);
        }
    }
}
