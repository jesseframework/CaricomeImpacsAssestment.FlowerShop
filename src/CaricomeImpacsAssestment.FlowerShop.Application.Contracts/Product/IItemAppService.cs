using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Product
{
    public interface IItemAppService : ICrudAppService<
                                        ItemDto,
                                        Guid,
                                        PagedAndSortedResultRequestDto,
                                        CreateUpdateItemDto>
    {
        Task<List<ItemsAllJoinDto>> GetAllItems();
        Task<ItemsAllJoinDto> GetItems(Guid Id);
        Task<List<ItemsAllJoinDto>> GetItemsByCatgory(ItemSearchDto input);
    }
}
