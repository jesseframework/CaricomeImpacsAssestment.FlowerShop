using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IRepository<AddressType, Guid> _addressTypeRepository;
        public AddressAppService(
            IRepository<Address, Guid> addressRepository,
            IRepository<AddressType, Guid> addressTypeRepository) : base(addressRepository)
        {
            _addressRepository = addressRepository;
            _addressTypeRepository = addressTypeRepository;
        }
        public async Task<AddressDto> GetBillingAddress(Guid Id)
        {
            
            AddressDto addressDto = new AddressDto();
            var getTypeData = await _addressTypeRepository.GetQueryableAsync();
            var getTypeQuery = getTypeData.SingleOrDefault(p => p.Type.Equals("Billing"));
            if (getTypeQuery != null)
            {
                var getAddressData = await _addressRepository.GetQueryableAsync();
                var getAddressQuery = getAddressData.SingleOrDefault(p => p.AddressTypeId == getTypeQuery.Id && p.Id==Id);
                if (getAddressQuery != null)
                {
                    addressDto = ObjectMapper.Map<Address, AddressDto>(getAddressQuery);
                }
                
            }          
            
            return addressDto;
        }
        public async Task<AddressDto> GetShippingAddress(Guid Id)
        {
            AddressDto addressDto = new AddressDto();
            var getTypeData = await _addressTypeRepository.GetQueryableAsync();
            var getTypeQuery = getTypeData.SingleOrDefault(p => p.Type.Equals("ShipTo"));
            if (getTypeQuery != null)
            {
                var getAddressData = await _addressRepository.GetQueryableAsync();
                var getAddressQuery = getAddressData.SingleOrDefault(p => p.AddressTypeId == getTypeQuery.Id && p.Id == Id);
                if (getAddressQuery != null)
                {
                    addressDto = ObjectMapper.Map<Address, AddressDto>(getAddressQuery);
                }

            }

            return addressDto;

        }
    }
}
