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
        Task<List<CustomerAllDto>> GetAllCustomersWithReference();
        Task<CustomerAllDto> GetCustomeWithReference();
        Task<CustomerAllDto> GetCustomeBillingAddressWithReference();
        Task<CustomerAllDto> GetCustomeShippingAddressWithReference();
        Task<PagedResultDto<CustomerAllDto>> GetManagmentCustomer();
        Task<CustomerAllDto> GetAllByIdCustomersWithReference(Guid Id);
    }
}
