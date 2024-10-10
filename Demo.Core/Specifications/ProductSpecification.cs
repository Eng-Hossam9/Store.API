using Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Specifications
{
    public class ProductSpecification:BaseSpecification<Product,int>
    {
        public ProductSpecification(int id) :base(p => p.Id == id)
        {
            ApplyInclude();
            
        }

        public ProductSpecification()
        {
            ApplyInclude();

        }

        private void ApplyInclude()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
        }
    }
}
