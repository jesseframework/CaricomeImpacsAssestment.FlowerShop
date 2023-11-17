using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Product
{
    public interface ITaxAppService : ICrudAppService<
                                        TaxDto,
                                        Guid,
                                        PagedAndSortedResultRequestDto,
                                        CreateUpdateTaxDto>
    {
    }
}
