using Demo.Core.DTO;
using Demo.Core.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.ServicesInterFaces
{
    public interface IProductService
    {
        Task<IEnumerable<TypeBrandDTO>> GetAllTypesAsync();
        Task<IEnumerable<ProductDTO>> GetAllProductAsync();
        Task<IEnumerable<TypeBrandDTO>> GetAllBrandsAsync();
        Task<ProductDTO> GetProductByIdAsync(int id);
    }
}
