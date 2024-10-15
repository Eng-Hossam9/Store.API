using Demo.Core.ServicesInterFaces;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.Service.Services.Caches
{
    public class CacheService : IcacheService
    {
        private readonly IDatabase _Database;

        public CacheService(IConnectionMultiplexer connection)
        {
            _Database = connection.GetDatabase();
        }
        public async Task<string> GetCacheKeyAsync(string cacheKey)
        {
           var CacheResult= await _Database.StringGetAsync(cacheKey);
            if (cacheKey.IsNullOrEmpty()) return null;

            return CacheResult.ToString();
        }

        public async Task SetCacheKeyAsync(string cacheKey, object response, TimeSpan ExpireTime)
        {
            if (response is null) return ;

            var option = new JsonSerializerOptions()
            {
                PropertyNamingPolicy=JsonNamingPolicy.CamelCase,
            };

         await _Database.StringSetAsync(cacheKey,JsonSerializer.Serialize(response,option), ExpireTime);
        }
    }
}
