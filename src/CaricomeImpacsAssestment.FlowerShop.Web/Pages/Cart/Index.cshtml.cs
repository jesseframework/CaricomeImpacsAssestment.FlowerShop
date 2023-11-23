using CaricomeImpacsAssestment.FlowerShop.Order;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using CaricomeImpacsAssestment.FlowerShop.RealTime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IHubContext<CartHub> _cartHubContext;
        private readonly IOrderDetailTempAppService _orderDetailTempAppService;
        private readonly ICookieTrackerAppService _cookieTrackerAppService;
       
        public IndexModel(ICookieTrackerAppService cookieTrackerAppService,
            IOrderDetailTempAppService orderDetailTempAppService,
            IHubContext<CartHub> cartHubContext)
        {
            _cookieTrackerAppService = cookieTrackerAppService;
            _orderDetailTempAppService = orderDetailTempAppService;
            _cartHubContext = cartHubContext;
        }

        [BindProperty(SupportsGet = true)]
        public List<OrderWithItemDataDto> checkOutList { get; set; }
        [BindProperty(SupportsGet = true)]
        public OrderDetailTempDto checkTotal { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var cartCookieValue = GetCookieValue("Browser_Cart_Session");
            var cookieUUID = await _cookieTrackerAppService.GetByCookieUUID(cartCookieValue);
            checkOutList = await _orderDetailTempAppService.GetShoppingCartForCheckOut(cookieUUID);
            checkTotal = await _orderDetailTempAppService.GetShoppingCartAmountByCookieId(cookieUUID);
            if(checkOutList != null && checkTotal != null)
            {
                          

                var cartMessage = new shoppingCartUpdateDto()
                {
                    LineTotal = checkTotal.LineTotal,
                    Quantity = checkTotal.Quantity,
                };

                await _cartHubContext.Clients.All.SendAsync("ReceiveCartUpdate", cartMessage);
            }

            if (checkOutList == null)
            {

                return RedirectToPage("/Shop");
            }
            return Page();
        }

        private string GetCookieValue(string cookieName)
        {
            return HttpContext.Request.Cookies[cookieName];
        }
    }
}
