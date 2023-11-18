using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Product
{
    public class ItemAppService : CrudAppService<
        Item,
        ItemDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateItemDto>,
        IItemAppService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<ProductGroup, Guid> _productgroupRepository;
        private readonly IRepository<ItemPrice, Guid> _itempricesRepository;
        private readonly IRepository<Item, Guid> _itemRepository;
        public ItemAppService(
            IRepository<Category, Guid> categoryRepository,
            IRepository<ProductGroup, Guid> productgroupRepository,
            IRepository<ItemPrice, Guid> itempricesRepository,
            IRepository<Item, Guid> itemRepository
            ) : base(itemRepository)
        {
            _categoryRepository = categoryRepository;
            _productgroupRepository = productgroupRepository;
            _itempricesRepository = itempricesRepository;
            _itemRepository = itemRepository;
        }
        public async Task<List<ItemsAllJoinDto>> GetAllItems()
        {

            //Am aware that this section of my code could have db perfromance issue if am dealing with large data.
            var items = await _itemRepository.GetListAsync();
            var categories = await _categoryRepository.GetListAsync();
            var productGroups = await _productgroupRepository.GetListAsync();
            var itemPrices = await _itempricesRepository.GetListAsync();

            var mapItems = ObjectMapper.Map<List<Item>, List<ItemDto>>(items);
            var mapCategories = ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories);
            var mapProductGroups = ObjectMapper.Map<List<ProductGroup>, List<ProductGroupDto>>(productGroups);
            var mapItemPrices = ObjectMapper.Map<List<ItemPrice>, List<ItemPriceDto>>(itemPrices);


            var queryResult = (from item in mapItems
                               join category in mapCategories on item.CategoryId equals category.Id
                               join productGroup in mapProductGroups on item.ProductGroupId equals productGroup.Id
                               join itemPrice in mapItemPrices on item.Id equals itemPrice.ItemId
                               where
                               itemPrice.IsActive == true
                               select new ItemsAllJoinDto
                               {

                                   category = category,
                                   productGroup = productGroup,
                                   price = itemPrice,
                                   allItems = item,

                               }).ToList();

            return queryResult;


        }
        public async Task<ItemsAllJoinDto> GetItems(Guid Id)
        {


            var items = await _itemRepository.GetListAsync();
            var categories = await _categoryRepository.GetListAsync();
            var productGroups = await _productgroupRepository.GetListAsync();
            var itemPrices = await _itempricesRepository.GetListAsync();

            var mapItems = ObjectMapper.Map<List<Item>, List<ItemDto>>(items);
            var mapCategories = ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories);
            var mapProductGroups = ObjectMapper.Map<List<ProductGroup>, List<ProductGroupDto>>(productGroups);
            var mapItemPrices = ObjectMapper.Map<List<ItemPrice>, List<ItemPriceDto>>(itemPrices);


            var queryResult = (from item in mapItems
                               join category in mapCategories on item.CategoryId equals category.Id
                               join productGroup in mapProductGroups on item.ProductGroupId equals productGroup.Id
                               join itemPrice in mapItemPrices on item.Id equals itemPrice.ItemId
                               where
                               itemPrice.IsActive == true
                               && item.Id == Id
                               select new ItemsAllJoinDto
                               {

                                   category = category,
                                   productGroup = productGroup,
                                   price = itemPrice,
                                   allItems = item,

                               }).SingleOrDefault();

            return queryResult;


        }

        public async Task<List<ItemsAllJoinDto>> GetItemsByCatgory(ItemSearchDto input)
        {
            bool IsFilterItemName = input.ItemName != null;
            bool IsFilterCategoryName = input.CategoryName != null;
            bool IsFilterProductName = input.ProductName != null;
                       

            var items = await _itemRepository.GetListAsync();
            var categories = await _categoryRepository.GetListAsync();
            var productGroups = await _productgroupRepository.GetListAsync();
            var itemPrices = await _itempricesRepository.GetListAsync();

            var mapItems = ObjectMapper.Map<List<Item>, List<ItemDto>>(items);
            var mapCategories = ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories);
            var mapProductGroups = ObjectMapper.Map<List<ProductGroup>, List<ProductGroupDto>>(productGroups);
            var mapItemPrices = ObjectMapper.Map<List<ItemPrice>, List<ItemPriceDto>>(itemPrices);


            var queryResult = (from item in mapItems
                               join category in mapCategories on item.CategoryId equals category.Id
                               join productGroup in mapProductGroups on item.ProductGroupId equals productGroup.Id
                               join itemPrice in mapItemPrices on item.Id equals itemPrice.ItemId
                               where
                               itemPrice.IsActive == true
                               && (IsFilterCategoryName ? (category.Title.Contains(input.CategoryName)) : true)
                               && (IsFilterProductName ? (productGroup.Name.Contains(input.ProductName)) : true)
                               && (IsFilterItemName ? (item.Name.Contains(input.ItemName)) : true)
                               select new ItemsAllJoinDto
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
