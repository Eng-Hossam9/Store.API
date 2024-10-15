using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.ServicesInterFaces
{
    public interface IcacheService
    {
        Task SetCacheKeyAsync(string cacheKey,object response,TimeSpan ExpireTime);
        Task<string> GetCacheKeyAsync(string cacheKey);
    }
}
