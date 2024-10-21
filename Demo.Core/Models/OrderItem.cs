using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Models
{
    public class OrderItem:BaseEntity<int>
    {
        public OrderItem(ProductItemOrder product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public OrderItem() { }
        public ProductItemOrder Product { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
