using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Customer
{
    public interface IAddressAppService : ICrudAppService<
                                        AddressDto,
                                        Guid,
                                        PagedAndSortedResultRequestDto,
                                        CreateUpdateAddressDto>
    {
        Task<AddressDto> GetBillingAddress(Guid Id);
        Task<AddressDto> GetShippingAddress(Guid Id);
    }
}
