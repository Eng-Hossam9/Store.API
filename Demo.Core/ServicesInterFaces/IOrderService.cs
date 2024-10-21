using Demo.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.ServicesInterFaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddres);

        Task<IEnumerable<Order>?> GetOrdersForSpecificUserAsync(string BuyerEmail);

        Task<Order?> GetOrderByIdAsync(string BuyerEmail,int OrderId);




    }
}
