using Demo.Core.Models;
using Demo.Core.ServicesInterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;
using Demo.Core.RepositoriesInterFaces;
using Product = Demo.Core.Models.Product;
using Microsoft.Extensions.Configuration;

namespace Demo.Service.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentService(IBasketRepository basketRepository,IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntentId(string BasketId)
        {

            StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];

            var PaymentService = new PaymentIntentService();
                
            var basket= await _basketRepository.GetBasketAsync(BasketId);
            if (basket == null)return null;

            var shippingprice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var delivary = await _unitOfWork.CreateRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
                shippingprice = delivary.Cost;
            }
            if (basket.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                var product = await _unitOfWork.CreateRepository<Product, int>().GetByIdAsync(item.Id);
                    if(item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }
                    
                }

            }
           var subtotal= basket.Items.Sum(i => (i.Price) * (i.Quantity));
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(subtotal * 100 + shippingprice * 100),
                    PaymentMethodTypes = new List<string>() { "card"},
                    Currency="usd"
                }; 
                paymentIntent =await PaymentService.CreateAsync(option);

                basket.PaymentIntentId=paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }

            else
            {
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(subtotal * 100 + shippingprice * 100),
                   
                };
                paymentIntent = await PaymentService.UpdateAsync(basket.PaymentIntentId, option);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
           await _basketRepository.UpdateOrAddBasketAsync(basket);
            if (basket == null) return null;
            return basket;  
        }
    }
}
