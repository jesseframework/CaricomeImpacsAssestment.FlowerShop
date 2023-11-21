using CaricomeImpacsAssestment.FlowerShop.Customer;
using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using CaricomeImpacsAssestment.FlowerShop.Order;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using CaricomeImpacsAssestment.FlowerShop.Product;
using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages.Signup
{
    public class IndexModel : PageModel
    {
        private readonly IOrderDetailTempAppService _orderDetailTempAppService;
        private readonly ICookieTrackerAppService _cookieTrackerAppService;
        private readonly ICountryAppService _countryAppService;
        public readonly ICurrencyAppService _currencyAppService;

        public IndexModel(ICookieTrackerAppService cookieTrackerAppService,
            IOrderDetailTempAppService orderDetailTempAppService,
            ICountryAppService countryAppService,
            ICurrencyAppService currencyAppService)
        {
            _cookieTrackerAppService = cookieTrackerAppService;
            _orderDetailTempAppService = orderDetailTempAppService;
            _countryAppService = countryAppService;
            _currencyAppService = currencyAppService;
        }
       
        [BindProperty(SupportsGet = true)]
        public List<OrderWithItemDataDto> checkOutList { get; set; }
        [BindProperty(SupportsGet = true)]
        public OrderDetailTempDto checkTotal { get; set; }
        [BindProperty(SupportsGet = true)]
        public ListResultDto<CountryDto> countryList { get; set; }
        [BindProperty(SupportsGet = true)]
        public ListResultDto<CurrencyDto> currencyList { get; set; }

        public async Task OnGetAsync()
        {
            
            var cartCookieValue = GetCookieValue("Browser_Cart_Session");
            var cookieUUID = await _cookieTrackerAppService.GetByCookieUUID(cartCookieValue);
            checkOutList = await _orderDetailTempAppService.GetShoppingCartForCheckOut(cookieUUID);
            checkTotal = await _orderDetailTempAppService.GetShoppingCartAmountByCookieId(cookieUUID);
            countryList = await _countryAppService.GetListAsync(_pg);
            currencyList = await _currencyAppService.GetListAsync(_pg);



            
        }

        private string GetCookieValue(string cookieName)
        {
            return HttpContext.Request.Cookies[cookieName];
        }

        private PagedAndSortedResultRequestDto _pg = new PagedAndSortedResultRequestDto()
        {
            SkipCount = 0,
            MaxResultCount = 10, 
            Sorting = ""
        };
    }
}
