using CaricomeImpacsAssestment.FlowerShop.Order;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages.CheckOut
{
    public class IndexModel : PageModel
    {
        private readonly IOrderHeaderAppService _orderHeaderAppService;
        private readonly ICookieTrackerAppService _cookieTrackerAppService;

        public IndexModel(IOrderHeaderAppService orderHeaderAppService, 
            ICookieTrackerAppService cookieTrackerAppService)
        {
            _orderHeaderAppService = orderHeaderAppService;
            _cookieTrackerAppService = cookieTrackerAppService;
        }

        [BindProperty(SupportsGet = true)]
        public OrderHeaderDto orderHeader { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {

            if (!User.Identity.IsAuthenticated)
            {

                HttpContext.Response.Redirect("/Signup");
                return new EmptyResult();
            }

            var cartCookieValue = GetCookieValue("Browser_Cart_Session");
            var cookieUUID = await _cookieTrackerAppService.GetByCookieUUID(cartCookieValue);
            orderHeader = await _orderHeaderAppService.GetOrderConfimTotalByCookieId(cookieUUID);
            if (orderHeader == null)
            {

                return RedirectToPage("/Shop");

            }

            return Page();
        }

        //ToDo This shoud not be repeat just wish i have more time
        private string GetCookieValue(string cookieName)
        {
            return HttpContext.Request.Cookies[cookieName];
        }
    }
}
