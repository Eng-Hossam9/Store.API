using Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Specifications
{
    public class OrderSpecification : BaseSpecification<Order, int>
    {
        public OrderSpecification(string BuyerEmail, int OrderId) : base(o => o.Id == OrderId && o.BuyerEmail == BuyerEmail)
        {
            
                Includes.Add(o => o.DeliveryMethod);
                Includes.Add(o => o.Items);
            
        }

        public OrderSpecification(string BuyerEmail):base(o=>o.BuyerEmail == BuyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
        }
    }
}
