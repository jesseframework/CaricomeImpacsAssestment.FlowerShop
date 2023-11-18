using CaricomeImpacsAssestment.FlowerShop.Order;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using CaricomeImpacsAssestment.FlowerShop.Product;
using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages.Product.Detail
{
    public class IndexModel : PageModel
    {
        private readonly IItemAppService _itemAppService;
        private readonly IOrderDetailTempAppService _orderDetailTempAppService;
        private readonly ICookieTrackerAppService _cookieTrackerAppService;

        public IndexModel(IItemAppService itemAppService,
            IOrderDetailTempAppService orderDetailTempAppService,
            ICookieTrackerAppService cookieTrackerAppService)
        {
            _itemAppService = itemAppService;
            _orderDetailTempAppService = orderDetailTempAppService;
            _cookieTrackerAppService = cookieTrackerAppService;
        }
        public ItemsAllJoinDto ItemsList = new ItemsAllJoinDto();

        [BindProperty(SupportsGet = true)]
        public Guid ProductId {get; set;}
        [BindProperty(SupportsGet = true)]
        public string Ck { get; set; }


        public async Task OnGetAsync(Guid id)
        {
            ProductId = id;
            ItemsList = await _itemAppService.GetItems(id);

            var cartCookieValue = GetCookieValue("Browser_Cart_Session");
            var cookieUUID = await _cookieTrackerAppService.GetByCookieUUID(cartCookieValue);
            
            Ck = cookieUUID.ToString();
        }

        public IActionResult OnPost(string action)
        {
            if (action == "viewCart")
            {
                return Redirect("Cart");
            }

            // Handle other actions or return to the same page
            return Page();
        }

        private string GetCookieValue(string cookieName)
        {
            return HttpContext.Request.Cookies[cookieName];
        }


    }
}
