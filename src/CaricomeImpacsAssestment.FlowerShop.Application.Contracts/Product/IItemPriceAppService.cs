using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Product
{
    public interface IItemPriceAppService : ICrudAppService<
                                        ItemPriceDto,
                                        Guid,
                                        PagedAndSortedResultRequestDto,
                                        CreateUpdateItemPriceDto>
    {
    }
}
