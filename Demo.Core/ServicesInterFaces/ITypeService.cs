using Demo.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.ServicesInterFaces
{
    public interface ITypeService
    {
        Task<IEnumerable<TypeBrandDTO>> GetAllTypesAsync();

    }
}
