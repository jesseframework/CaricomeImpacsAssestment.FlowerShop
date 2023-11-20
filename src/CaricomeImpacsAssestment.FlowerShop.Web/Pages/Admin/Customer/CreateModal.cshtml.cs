using CaricomeImpacsAssestment.FlowerShop.Customer;
using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages.Admin.Customer
{
    
    public class CreateModalModel : PageModel
    {
        [BindProperty]
        public CreateUpdateCustomerAccountDto _customerAccount { get; set; }
        private readonly ICustomerAccountAppService _customerAccountAppService;

        public CreateModalModel(ICustomerAccountAppService customerAccountAppService)
        {
            _customerAccountAppService = customerAccountAppService;
        }

        public void OnGet()
        {
            _customerAccount = new CreateUpdateCustomerAccountDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _customerAccountAppService.CreateAsync(_customerAccount);
            return Page();
        }
    }
}
