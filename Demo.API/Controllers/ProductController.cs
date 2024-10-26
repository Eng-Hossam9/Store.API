using Demo.API.Attributes;
using Demo.Core.DTO.Product;
using Demo.Core.productParams;
using Demo.Core.ProductResponse;
using Demo.Core.ServicesInterFaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProductsController : ControllerBase
    {
        private readonly IProductService _product;
        private readonly ITypeService _Type;
        private readonly IBrandService _Brand;

        public ProductsController(IProductService product, ITypeService type, IBrandService brand)
        {
            _product = product;
            _Type = type;
            _Brand = brand;
        }



        [HttpGet]
        [Cache(100)]
        public async Task<IActionResult> GetAllProduct([FromQuery]ProductParams productParams)
        {
            var Result = await _product.GetAllProductAsync(productParams);
            return Ok(Result);

        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var Result = await _Brand.GetAllBrandsAsync();
            return Ok(Result);

        }
        [HttpGet("types")]
        public async Task<IActionResult> GetAllType()
        {
            var Result = await _Type.GetAllTypesAsync();
            return Ok(Result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByid(int? id)
        {
            if (id == null) return BadRequest();
            var Result = await _product.GetProductByIdAsync(id.Value);
            if (Result == null) return NotFound();
            return Ok(Result);

        }
    }
}
