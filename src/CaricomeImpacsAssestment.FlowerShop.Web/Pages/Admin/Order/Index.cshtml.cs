using CaricomeImpacsAssestment.FlowerShop.Customer;
using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using CaricomeImpacsAssestment.FlowerShop.Order;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages.Admin.Order
{
    public class IndexModel : PageModel
    {
        private readonly IOrderHeaderAppService _orderHeaderAppService;

        public IndexModel(IOrderHeaderAppService orderHeaderAppService)
        {
            _orderHeaderAppService = orderHeaderAppService;
        }


        public List<OrderWithItemDataDto> OrderTrackingList = new List<OrderWithItemDataDto>();


        public async Task OnGetAsync()
        {
            OrderTrackingList = await _orderHeaderAppService.GetOrderAllOrderSum();
        }





    }
}
