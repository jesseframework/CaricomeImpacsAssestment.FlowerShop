using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Product
{
    public interface IProductGroupAppService : ICrudAppService<
                                        ProductGroupDto,
                                        Guid,
                                        PagedAndSortedResultRequestDto,
                                        CreateUpdateProductGroupDto>
    {
    }
}
