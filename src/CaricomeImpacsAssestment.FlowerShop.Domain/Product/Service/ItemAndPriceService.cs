using CaricomeImpacsAssestment.FlowerShop.Product.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Product.Service
{
    public class ItemAndPriceService : DomainService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<ProductGroup, Guid> _productgroupRepository;
        private readonly IRepository<ItemPrice, Guid> _itempricesRepository;
        private readonly IRepository<Item, Guid> _itemRepository;
        public ItemAndPriceService(
            IRepository<Category, Guid> categoryRepository,
            IRepository<ProductGroup, Guid> productgroupRepository,
            IRepository<ItemPrice, Guid> itempricesRepository,
            IRepository<Item, Guid> itemRepository
            )
        {
            _categoryRepository = categoryRepository;
            _productgroupRepository = productgroupRepository;
            _itempricesRepository = itempricesRepository;
            _itemRepository = itemRepository;


        }
        public async Task<List<ItemsAllJoin>> ItemWithAll()
        {


            var items = await _itemRepository.GetListAsync();
            var categories = await _categoryRepository.GetListAsync();
            var productGroups = await _productgroupRepository.GetListAsync();
            var itemPrices = await _itempricesRepository.GetListAsync();


            var queryResult = (from item in items
                               join category in categories on item.CategoryId equals category.Id
                               join productGroup in productGroups on item.ProductGroupId equals productGroup.Id
                               join itemPrice in itemPrices on item.Id equals itemPrice.ItemId
                               where
                               itemPrice.IsActive == true
                               select new ItemsAllJoin
                               {

                                   category = category,
                                   productGroup = productGroup,
                                   price = itemPrice,
                                   allItems = item,

                               }).ToList();

            return queryResult;
        }


    }

}
