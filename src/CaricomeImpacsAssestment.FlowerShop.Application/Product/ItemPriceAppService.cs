using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Product
{
    public class ItemPriceAppService : CrudAppService<
        ItemPrice,
        ItemPriceDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateItemPriceDto>,
        IItemPriceAppService
    {
        private readonly IRepository<ItemPrice, Guid> _itemPriceRepository;
        public ItemPriceAppService(IRepository<ItemPrice, Guid> itemPriceRepository) : base(itemPriceRepository)
        {
            _itemPriceRepository = itemPriceRepository;
        }
    }
}
