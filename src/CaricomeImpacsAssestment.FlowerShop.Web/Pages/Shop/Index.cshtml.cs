using CaricomeImpacsAssestment.FlowerShop.Order;
using CaricomeImpacsAssestment.FlowerShop.Product;
using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly IItemAppService _itemAppService;
        private readonly IOrderDetailTempAppService _orderDetailTempAppService;
        private readonly ICookieTrackerAppService _cookieTrackerAppService;
        


        public IndexModel(           
            IItemAppService itemAppService,
             IOrderDetailTempAppService orderDetailTempAppService,
            ICookieTrackerAppService cookieTrackerAppService)
        {
            _itemAppService = itemAppService;
            _orderDetailTempAppService = orderDetailTempAppService;
            _cookieTrackerAppService = cookieTrackerAppService;
        }
        public List<ItemsAllJoinDto> ItemsList = new List<ItemsAllJoinDto>();

        //[BindProperty(SupportsGet = true)]
        //public ItemSearchDto searchDto { get; set; }

        [BindProperty(SupportsGet = true)]
        public string categoryName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string productGroupName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string itemName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Ck { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public string Param2 { get; set; }
        public async Task OnGetAsync()
        {
            string cookieName = "Browser_Cart_Session";
            string cookieValue = Guid.NewGuid().ToString();
            int expirationDays = 3;

            

            if (!HttpContext.Request.Cookies.ContainsKey(cookieName))
            {
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(expirationDays),
                    Secure = true,  //Make is safe bro
                    HttpOnly = true //make sure JavaScript cant see Cookie Jermaine
                };

                Response.Cookies.Append(cookieName, cookieValue, options);
            }

            var searchParam = new ItemSearchDto()
            {
                CategoryName = categoryName,
                ProductName = productGroupName,
                ItemName = itemName

            };

            //SearchDto ??= new ItemSearchDto();
            ItemsList = await _itemAppService.GetItemsByCatgory(searchParam);

            var cartCookieValue = GetCookieValue("Browser_Cart_Session");
            var cookieUUID = await _cookieTrackerAppService.GetByCookieUUID(cartCookieValue);

            Ck = cookieUUID.ToString();
        }

        private string GetCookieValue(string cookieName)
        {
            return HttpContext.Request.Cookies[cookieName];
        }


    }
}
