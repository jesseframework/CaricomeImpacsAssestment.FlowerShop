using CaricomeImpacsAssestment.FlowerShop.Customer;
using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages.Admin.Customer.Update
{
    public class IndexModel : PageModel
    {
        //public void OnGet()
        //{
        //}

        private readonly ICustomerAccountAppService _customerAccountAppService;
        private readonly ICountryAppService _countryAppService;
        public readonly ICurrencyAppService _currencyAppService;

        public IndexModel(ICustomerAccountAppService customerAccountAppService,
            ICountryAppService countryAppService,
            ICurrencyAppService currencyAppService)
        {
            _customerAccountAppService = customerAccountAppService;
            _countryAppService = countryAppService;
            _currencyAppService = currencyAppService;
        }
        public CustomerAllDto customerAllList = new CustomerAllDto();

        [BindProperty(SupportsGet = true)]
        public Guid CustomertId { get; set; }
        [BindProperty(SupportsGet = true)]
        public ListResultDto<CountryDto> countryList { get; set; }
        [BindProperty(SupportsGet = true)]
        public ListResultDto<CurrencyDto> currencyList { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            CustomertId = id;
            customerAllList = await _customerAccountAppService.GetAllByIdCustomersWithReference(id);
            countryList = await _countryAppService.GetListAsync(_pg);
            currencyList = await _currencyAppService.GetListAsync(_pg);

        }
        private PagedAndSortedResultRequestDto _pg = new PagedAndSortedResultRequestDto()
        {
            SkipCount = 0,
            MaxResultCount = 10,
            Sorting = ""
        };


    }
}
