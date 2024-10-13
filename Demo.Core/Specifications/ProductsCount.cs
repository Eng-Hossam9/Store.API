using Demo.Core.Models;
using Demo.Core.productParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Specifications
{
    public class ProductsCount : BaseSpecification<Product, int>
    {
        public ProductsCount(ProductParams productParams)
           : base(

               p =>
               (string.IsNullOrEmpty(productParams.Search) || p.Name.ToLower().Contains(productParams.Search))
               &&
               (!productParams.brandId.HasValue || p.BrandId == productParams.brandId)
               &&
               (!productParams.typeId.HasValue || p.TypeId == productParams.typeId)

                )
        {
        }
    }
}


