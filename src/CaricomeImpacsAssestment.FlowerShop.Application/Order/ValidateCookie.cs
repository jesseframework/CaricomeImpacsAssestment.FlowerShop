using CaricomeImpacsAssestment.FlowerShop.Order.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaricomeImpacsAssestment.FlowerShop.Order
{
    public class ValidateCookie
    {
        private readonly CookieService _cookieService;
        private readonly UserCookieManager _userCookieManager;
        private readonly BrowserInfomation _browserInfomation;

        public ValidateCookie(BrowserInfomation browserInfomation,
            CookieService cookieService,
            UserCookieManager userCookieManager)
        {
            _cookieService = cookieService;
            _userCookieManager = userCookieManager;
            _browserInfomation = browserInfomation;
        }
        public async Task<Guid> IsCookieValid()
        {
            Guid cookieId = Guid.NewGuid();
            var cookieValue = _cookieService.GetBrowserCookie("Browser_Cart_Session");
            if (!string.IsNullOrEmpty(cookieValue))
            {
                var (Domain, Path) = _browserInfomation.ProcessRequest();
                if (!string.IsNullOrEmpty(Domain) && !string.IsNullOrEmpty(Path))
                {
                    cookieId = await _userCookieManager.SaveCookieAsync(cookieValue, Domain, Path);
                }

            }
            return cookieId;
        }
    }
}
