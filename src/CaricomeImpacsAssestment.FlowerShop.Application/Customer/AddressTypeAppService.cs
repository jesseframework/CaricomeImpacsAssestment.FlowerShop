using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Customer
{
    public class AddressTypeAppService : CrudAppService<
        AddressType,
        AddressTypeDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateAddressTypeDto>,
        IAddressTypeAppService
    {
        private readonly IRepository<AddressType, Guid> _addressTypeRepository;
        public AddressTypeAppService(
            IRepository<AddressType, Guid> addressTypeRepository) : base(addressTypeRepository) 
        {
            _addressTypeRepository = addressTypeRepository;
        }
    }
}
