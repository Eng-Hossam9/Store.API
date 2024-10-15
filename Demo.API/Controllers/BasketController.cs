using AutoMapper;
using Demo.API.Errors;
using Demo.Core.DTO.Basket;
using Demo.Core.Models;
using Demo.Core.RepositoriesInterFaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basket;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basket,IMapper mapper)
        {
           _basket = basket;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetBasket(string id)
        {
            if (id == null) { return BadRequest(new ApiErrorResponse(400)); }

            var basket =await _basket.GetBasketAsync(id);

            if (basket == null)
            {
                new CustomerBasket()
                {
                    Id = id,    
                };
            }
            return Ok(basket);  

        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasket(CustomerBasketDTO customerBasketDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiErrorResponse(400)); }


         var basket=  await _basket.UpdateOrAddBasketAsync(_mapper.Map<CustomerBasket>(customerBasketDTO));

            if (basket == null) 
            {
                return BadRequest(new ApiErrorResponse(400));
            }
            return Ok(basket);
        }


        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
           await _basket.DeleteBasketAsync(id);

        }


    }
}
