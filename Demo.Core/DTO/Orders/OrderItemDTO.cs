using Demo.Core.Models;

namespace Demo.Core.DTO.Orders
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }   
        public string PictureUrl { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}