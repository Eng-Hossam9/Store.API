using Demo.Core.Models;
using Demo.Core.productParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product, int>
    {
        public ProductSpecification(int id) : base(
            
            p => p.Id == id
  
            )
        {
            ApplyInclude();

        }

        public ProductSpecification(ProductParams productParams)
            :base(

                p =>
                (string.IsNullOrEmpty(productParams.Search)||  p.Name.ToLower().Contains(productParams.Search))
                &&
                (!productParams.brandId.HasValue || p.BrandId == productParams.brandId) 
                &&
                (!productParams.typeId.HasValue || p.TypeId== productParams.typeId)
                
                 
                 )
        {
            if (!string.IsNullOrEmpty(productParams.sort))
            {
                switch (productParams.sort)
                {
                    case "price":
                        AddOrderByAsc(p=>p.Price);
                            break;
                    case "pricedesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    case "name":
                        AddOrderByAsc(p => p.Name);

                        break;
                    default:
                        AddOrderByAsc(p => p.Id);

                        break;
                }

            }
            else
            {
                Orderby = p => p.Id;
            }

            ApplyInclude();

            ApplyPagination(productParams.pageSize * (productParams.pageIndex - 1), productParams.pageSize);

        }

       
        
        private void ApplyInclude()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
        }


    }
}
