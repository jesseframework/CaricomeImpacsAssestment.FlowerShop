using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Customer
{
    public class AddressAppService : CrudAppService<
        Address,
        AddressDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateAddressDto>,
        IAddressAppService
    {
        private readonly IRepository<Address, Guid> _addressRepository;
        public AddressAppService(
            IRepository<Address, Guid> addressRepository) : base(addressRepository)
        {
            _addressRepository = addressRepository;
        }
    }
}
