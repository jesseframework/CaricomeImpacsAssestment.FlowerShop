using CaricomeImpacsAssestment.FlowerShop.AppLogger;
using CaricomeImpacsAssestment.FlowerShop.Customer;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Order
{
    public class CompleteAndCheckOutAppService : CrudAppService<
        OrderHeader,
        OrderHeaderDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateOrderHeaderDto>,
        IOrderHeaderAppService
    {
        private readonly IRepository<OrderHeader, Guid> _orderHeaderRepository;
        private readonly ICustomerAccountAppService _customerAccountAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICookieTrackerAppService _cookieTrackerAppService;
        private readonly IRepository<CookieTracker, Guid> _cookieTrackerRepository;
        public CompleteAndCheckOutAppService(
            IRepository<OrderHeader, Guid> orderHeaderRepository,
            ICustomerAccountAppService customerAccountAppService,
            IHttpContextAccessor httpContextAccessor,
            ICookieTrackerAppService cookieTrackerAppService,
            IRepository<CookieTracker, Guid> cookieTrackerRepository) : base(orderHeaderRepository) 
        {
            _orderHeaderRepository = orderHeaderRepository;
            _customerAccountAppService = customerAccountAppService;
            _httpContextAccessor = httpContextAccessor;
            _cookieTrackerAppService = cookieTrackerAppService;
            _cookieTrackerRepository = cookieTrackerRepository;
        }
        public async Task<ResponseStatusCodesDto> CreateCompleteOrderAsync()
        {
            var customerAccountData = await _customerAccountAppService.GetCustomeWithRefrence();
            if(customerAccountData == null)
            {
                return new ResponseStatusCodesDto
                {
                    StatusCode = customerAccountData == null ? 700 : 800,
                    StatusMessage = "No Customer Found"
                };
            }

            //GetCookie
            CookieService cookieService = new CookieService(_httpContextAccessor);
            var cookieValue = cookieService.GetBrowserCookie;
            var cookieData = await _cookieTrackerRepository.GetAsync(p => p.Value.Equals(cookieValue));
            if(cookieData == null)
            {
                return new ResponseStatusCodesDto
                {
                    StatusCode = cookieData == null ? 700 : 800,
                    StatusMessage = "No Cookie Data Found"
                };
            }


            var saveOrderHeader = new CreateUpdateOrderHeaderDto()
            {
                OrderNo = "",
                CookieTrackerId = cookieData.Id,
                OrderDate = DateTime.Now,

            };

            return new ResponseStatusCodesDto
            {
                StatusCode = 800,
                StatusMessage = "Order complete successfull"
            };
        }
    }
}
