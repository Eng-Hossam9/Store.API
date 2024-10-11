using Demo.Core.DTO;
using Demo.Core.DTO.Product;
using Demo.Core.productParams;
using Demo.Core.ProductResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.ServicesInterFaces
{
    public interface IProductService
    {
        Task<ProductRespons<ProductDTO>> GetAllProductAsync(ProductParams productParams);
        Task<ProductDTO> GetProductByIdAsync(int id);
    }
}
