using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace CaricomeImpacsAssestment.FlowerShop.Order
{
    public class CookieService : ITransientDependency
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetBrowserCookie(string cookieName)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(cookieName, out string cookieValue))
            {
                return cookieValue;
            }

            return null;
        }
    }
}
