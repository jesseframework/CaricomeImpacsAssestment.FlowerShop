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
        

        public IndexModel(           
            IItemAppService itemAppService)
        {
            _itemAppService = itemAppService;            
        }
        public List<ItemsAllJoinDto> ItemsList = new List<ItemsAllJoinDto>();
        public async Task OnGetAsync()
        {
            string cookieName = "Browser_Cart_Session";
            string cookieValue = Guid.NewGuid().ToString();
            int expirationDays = 3;

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(expirationDays),
                Secure = true,  //Make is safe bro
                HttpOnly = true //make sure JavaScript cant see Cookie Jermaine
            };

            Response.Cookies.Append(cookieName, cookieValue, options);

            ItemsList = await _itemAppService.GetAllItems();
        }
       
        
    }
}
