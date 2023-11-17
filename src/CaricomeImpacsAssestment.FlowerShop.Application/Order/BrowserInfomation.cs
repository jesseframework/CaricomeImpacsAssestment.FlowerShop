using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CaricomeImpacsAssestment.FlowerShop.Order
{
    public class BrowserInfomation : ITransientDependency
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BrowserInfomation(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public (string DomainName, string FullUrl) ProcessRequest()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                
                string domainName = httpContext.Request.Host.Value;

                
                string fullUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}{httpContext.Request.QueryString}";

                return (domainName, fullUrl);
            }

            return (null, null);
        }
    }
}
