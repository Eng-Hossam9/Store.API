using Demo.Core.DTO.Product;
using Demo.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.ServicesInterFaces
{
    public interface IBrandService
    {
        Task<IEnumerable<TypeBrandDTO>> GetAllBrandsAsync();
    }
}
