using Demo.Core.Models;
using Demo.Core.RepositoriesInterFaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.Repository.Repositories.Baskets
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _DataBase;

        public BasketRepository(IConnectionMultiplexer Redis)
        {

            _DataBase = Redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
          return await  _DataBase.KeyDeleteAsync(BasketId);
                }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
           var Basket= await _DataBase.StringGetAsync(BasketId);
            return Basket.IsNullOrEmpty?null:JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdateOrAddBasketAsync(CustomerBasket Basket)
        {
            var Flag =await _DataBase.StringSetAsync(Basket.Id,JsonSerializer.Serialize(Basket) , TimeSpan.FromDays(30));
            if (Flag is false) return null;
             return await GetBasketAsync(Basket.Id);

        }
    }
}
