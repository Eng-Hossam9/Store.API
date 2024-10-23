using Demo.Core.Models;
using Demo.Core.RepositoriesInterFaces;
using Demo.Core.ServicesInterFaces;
using Demo.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Service.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }
        public async Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddres)
        {
            var BasketItem = await _basketRepository.GetBasketAsync(BasketId);
            if (BasketItem == null) return null;
            var Itemes = new List<OrderItem>();
            if (BasketItem.Items.Count() > 0)
            {
                foreach (var item in BasketItem.Items)
                {
                    var product = await _unitOfWork.CreateRepository<Product, int>().GetByIdAsync(item.Id);
                    var ProductOrderItem = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                    var orderitem = new OrderItem(ProductOrderItem, product.Price, item.Quantity);
                    Itemes.Add(orderitem);

                }

            }
            var DeliveryMethod = await _unitOfWork.CreateRepository<DeliveryMethod, int>().GetByIdAsync(DeliveryMethodId);
            var subtotatl = Itemes.Sum(i => i.Price * i.Quantity);
            var order = new Order(BuyerEmail, ShippingAddres, DeliveryMethod, Itemes, subtotatl, "");

            await _unitOfWork.CreateRepository<Order, int>().AddAsync(order);


            try
            {
                var result = await _unitOfWork.CompletSaveChangesAsync();
                if (result <= 0) return null;
            }
            catch (Exception ex)
            {
                // Log the exception (use a logger or just console for testing)
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                return null;
            }
         
            return order;
        }

        public async Task<Order?> GetOrderByIdAsync(string BuyerEmail, int OrderId)
        {
            var spec = new OrderSpecification(BuyerEmail, OrderId);

            var Order =await _unitOfWork.CreateRepository<Order, int>().GetByIdWihSpecAsync(spec);
            if (Order is null) return null;
            return Order;
        }

        public Task<IEnumerable<Order>?> GetOrdersForSpecificUserAsync(string BuyerEmail)
        {
            var spec = new OrderSpecification(BuyerEmail);

            var Order = _unitOfWork.CreateRepository<Order, int>().GetAllWihSpecAsync(spec);
            if (Order is null) return null;
            return Order;
        }
    }
}
