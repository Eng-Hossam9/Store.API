using Demo.Core.Models;
using Demo.Core.RepositoriesInterFaces;
using Demo.Core.ServicesInterFaces;
using Demo.Core.Specifications;
using Demo.Service.Services.Payment;
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
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
            _paymentService = paymentService;
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

            if (!string.IsNullOrEmpty(BasketItem.PaymentIntentId))
            {
                var orderspecwithpayment = new OrderSpecificationWithPayment(BasketItem.PaymentIntentId);
                var Exist = await _unitOfWork.CreateRepository<Order, int>().GetByIdWihSpecAsync(orderspecwithpayment);

                if (Exist != null)
                {
                    _unitOfWork.CreateRepository<Order, int>().Delete(Exist);
                }
            }


            var basket =await _paymentService.CreateOrUpdatePaymentIntentId(BasketId);


            var order = new Order(BuyerEmail, ShippingAddres, DeliveryMethod, Itemes, subtotatl, basket.PaymentIntentId);

            await _unitOfWork.CreateRepository<Order, int>().AddAsync(order);


            try
            {
                var result = await _unitOfWork.CompletSaveChangesAsync();
                if (result <= 0) return null;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                return null;
            }

            return order;
        }

        public async Task<Order?> GetOrderByIdAsync(string BuyerEmail, int OrderId)
        {
            var spec = new OrderSpecification(BuyerEmail, OrderId);

            var Order = await _unitOfWork.CreateRepository<Order, int>().GetByIdWihSpecAsync(spec);
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
