using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Customer
{
    public interface IAddressTypeAppService : ICrudAppService<
                                        AddressTypeDto,
                                        Guid,
                                        PagedAndSortedResultRequestDto,
                                        CreateUpdateAddressTypeDto>
    {
    }
}
