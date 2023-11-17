using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Customer
{
    public interface ICustomerAccountAppService : ICrudAppService<
                                        CustomerAccountDto,
                                        Guid,
                                        PagedAndSortedResultRequestDto,
                                        CreateUpdateCustomerAccountDto>
    {
        Task<List<CustomerAllDto>> GetAllCustomersWithRefrence();
        Task<CustomerAllDto> GetCustomeWithRefrence();
    }
}
