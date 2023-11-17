using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Customer
{
    public interface IContactAppService : ICrudAppService<
                                        ContactDto,
                                        Guid,
                                        PagedAndSortedResultRequestDto,
                                        CreateUpdateContactDto>
    {

    }
}
