using Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Specifications
{
    public class OrderSpecificationWithPayment:BaseSpecification<Order,int>
    {
        public OrderSpecificationWithPayment(string paymentintentId ):base(o=>o.PaymentIntentId== paymentintentId)
        {
            Includes.Add(o=>o.Items);
            Includes.Add(o=>o.DeliveryMethod);
        }
    }
}
