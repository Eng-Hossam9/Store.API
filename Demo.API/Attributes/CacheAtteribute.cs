using Demo.Core.ServicesInterFaces;
using Demo.Service.Services.Caches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace Demo.API.Attributes
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int Expirtime;

        public CacheAttribute(int expirtime)
        {
            Expirtime = expirtime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CasheService = context.HttpContext.RequestServices.GetRequiredService<IcacheService>();

            var CachKey = GenerateCacheKey(context.HttpContext.Request);

            var CachResponse=await CasheService.GetCacheKeyAsync(CachKey);

            if(!string.IsNullOrEmpty(CachResponse))
            {
                var ContentResult = new ContentResult()
                {
                    Content = CachResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result= ContentResult;
                return;
            }

            var ExecutionContext= await next.Invoke();
            if (ExecutionContext.Result is OkObjectResult Response)
            {
                await CasheService.SetCacheKeyAsync(CachKey, Response.Value, TimeSpan.FromSeconds(Expirtime));

            }


        }

        private string GenerateCacheKey(HttpRequest Request) 
        {
            var CacheKey=new StringBuilder();
            CacheKey.Append(Request.Path);

            foreach (var (key,value) in Request.Query.OrderBy(k=>k.Key))
            {
                CacheKey.Append($"|{key}-{value}");
            }


            return CacheKey.ToString(); 
        }
    }
}
