using Demo.Core.ServicesInterFaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _product;

        public ProductController(IProductService product)
        {
            _product = product;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var Result = await _product.GetAllProductAsync();
            return Ok(Result);

        }

        [HttpGet("AllBrands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var Result = await _product.GetAllBrandsAsync();
            return Ok(Result);

        }
        [HttpGet("AllType")]
        public async Task<IActionResult> GetAllType()
        {
            var Result = await _product.GetAllTypesAsync();
            return Ok(Result);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByid(int? id)
        {
            if(id == null)return BadRequest();
            var Result = await _product.GetProductByIdAsync(id.Value);
            if(Result == null) return NotFound();
            return Ok(Result);

            }
    }
}
