using Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTO.Orders
{
    public class OrderResponseDTO
    {

       public int Id { get; set; }  

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        public string Status { get; set; } 

        public AddressDTO ShippingAddress { get; set; }


        public string DeliveryMethod { get; set; }
        public Decimal DeliveryMethodCost { get; set; }

        public ICollection<OrderItemDTO> Items { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total {  get; set; } 

        public string? PaymentIntentId { get; set; }=string.Empty;
    }
}
