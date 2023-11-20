using CaricomeImpacsAssestment.FlowerShop.Customer;
using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using CaricomeImpacsAssestment.FlowerShop.Order;
using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using CaricomeImpacsAssestment.FlowerShop.Product;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages.Admin.Customer
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

