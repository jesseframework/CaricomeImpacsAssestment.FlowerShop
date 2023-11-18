using CaricomeImpacsAssestment.FlowerShop.Order;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IOrderDetailTempAppService _orderDetailTempAppService;
        private readonly ICookieTrackerAppService _cookieTrackerAppService;
       
        public IndexModel(ICookieTrackerAppService cookieTrackerAppService,
            IOrderDetailTempAppService orderDetailTempAppService)
        {
            _cookieTrackerAppService = cookieTrackerAppService;
            _orderDetailTempAppService = orderDetailTempAppService;
        }

        [BindProperty(SupportsGet = true)]
        public List<OrderWithItemDataDto> checkOutList { get; set; }
        [BindProperty(SupportsGet = true)]
        public OrderDetailTempDto checkTotal { get; set; }

        public async Task OnGetAsync()
        {
            var cartCookieValue = GetCookieValue("Browser_Cart_Session");
            var cookieUUID = await _cookieTrackerAppService.GetByCookieUUID(cartCookieValue);
            checkOutList = await _orderDetailTempAppService.GetShoppingCartForCheckOut(cookieUUID);
            checkTotal = await _orderDetailTempAppService.GetShoppingCartAmountByCookieId(cookieUUID);
        }

        private string GetCookieValue(string cookieName)
        {
            return HttpContext.Request.Cookies[cookieName];
        }
    }
}
