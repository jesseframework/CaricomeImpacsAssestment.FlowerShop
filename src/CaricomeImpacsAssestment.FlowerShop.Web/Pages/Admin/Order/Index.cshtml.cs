using CaricomeImpacsAssestment.FlowerShop.Customer;
using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages.Admin.Order
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerAccountAppService _customerAccountAppService;

        public IndexModel(ICustomerAccountAppService customerAccountAppService)
        {
            _customerAccountAppService = customerAccountAppService;
        }


        public List<CustomerAllDto> CustomerAccountList = new List<CustomerAllDto>();


        public async Task OnGetAsync()
        {
            CustomerAccountList = await _customerAccountAppService.GetAllCustomersWithReference();
        }





    }
}
