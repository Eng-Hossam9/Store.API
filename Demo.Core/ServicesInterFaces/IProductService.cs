﻿using Demo.Core.DTO;
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
        Task<IEnumerable<ProductDTO>> GetAllProductAsync();
        Task<ProductDTO> GetProductByIdAsync(int id);
    }
}
