using CaricomeImpacsAssestment.FlowerShop.Customer;
using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using CaricomeImpacsAssestment.FlowerShop.Order;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages.Admin.Order.Detail
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerAccountAppService _customerAccountAppService;
        private readonly ICountryAppService _countryAppService;
        private readonly ICurrencyAppService _currencyAppService;
        private readonly IAddressAppService _addressAppService;
        private readonly IContactAppService _contactAppService;
        private readonly IOrderHeaderAppService _orderHeaderAppService;

        public IndexModel(ICustomerAccountAppService customerAccountAppService,
                          ICountryAppService countryAppService,
                          ICurrencyAppService currencyAppService,
                          IAddressAppService addressAppService,
                          IContactAppService contactAppService,
                          IOrderHeaderAppService orderHeaderAppService)
        {
            _customerAccountAppService = customerAccountAppService;
            _countryAppService = countryAppService;
            _currencyAppService = currencyAppService;
            _addressAppService = addressAppService;
            _contactAppService = contactAppService;
            _orderHeaderAppService = orderHeaderAppService;
        }

        public CustomerAccountDto CustomerList { get; private set; }
        public ContactDto ContactList { get; private set; }
        public AddressDto SelectedBillingAddress { get; private set; }
        public AddressDto SelectedShippingAddress { get; private set; }
        public ListResultDto<CountryDto> CountryList { get; private set; }
        public ListResultDto<CurrencyDto> CurrencyList { get; private set; }
        public OrderHeaderDto OrderHeaderGroup { get; private set; }
        public List<OrderWithItemDataDto> OrderDetailList { get; private set; }

        [BindProperty(SupportsGet = true)]
        public Guid SelectedBillingCountryId { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid SelectedShippingCountryId { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid SelectedCurrencyId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string OrderNo { get; set; }


        private PagedAndSortedResultRequestDto _pg = new PagedAndSortedResultRequestDto
        {
            SkipCount = 0,
            MaxResultCount = 10,
            Sorting = ""
        };

        public async Task OnGetAsync(string orderno)
        {
            OrderHeaderGroup = await _orderHeaderAppService.GetOrderHeaderByOrderNo(OrderNo);
            if (OrderHeaderGroup != null)
            {
                OrderDetailList = await _orderHeaderAppService.GetOrderAllOrderWithItems(OrderHeaderGroup.Id);
                CustomerList = await _customerAccountAppService.GetAsync(OrderHeaderGroup.CustomerAccountId);
                SelectedBillingAddress = await _addressAppService.GetBillingAddress(CustomerList.BillingAddressId);
                SelectedShippingAddress = await _addressAppService.GetShippingAddress(CustomerList.ShippingAddressId);
                if (CustomerList != null)
                {
                    ContactList = await _contactAppService.GetAsync(CustomerList.ContactId);
                    CountryList = await _countryAppService.GetListAsync(_pg);
                    CurrencyList = await _currencyAppService.GetListAsync(_pg);
                }
            }
            



        }
    }
}

