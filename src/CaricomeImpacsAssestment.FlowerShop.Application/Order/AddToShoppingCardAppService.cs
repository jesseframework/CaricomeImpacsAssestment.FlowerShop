using CaricomeImpacsAssestment.FlowerShop.AppLogger;
using CaricomeImpacsAssestment.FlowerShop.Customer;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using CaricomeImpacsAssestment.FlowerShop.Order.Manager;
using CaricomeImpacsAssestment.FlowerShop.Payment;
using CaricomeImpacsAssestment.FlowerShop.Product;
using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace CaricomeImpacsAssestment.FlowerShop.Order
{
    public class AddToShoppingCardAppService : CrudAppService<
        OrderDetailTemp,
        OrderDetailTempDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateOrderDetailTempDto>,
        IOrderDetailTempAppService
    {
        
        
        private readonly IRepository<OrderDetailTemp, Guid> _orderdetailTeamRepository;       
        private readonly IItemAppService _itemAppService;        
        private readonly CookieService _cookieService;
        private readonly IRepository<Coupon, Guid> _couponRepository;
        private readonly UserCookieManager _userCookieManager;        
        private readonly BrowserInfomation _browserInfomation;
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<ProductGroup, Guid> _productgroupRepository;
        private readonly IRepository<ItemPrice, Guid> _itempricesRepository;
        private readonly IRepository<Item, Guid> _itemRepository;
        public AddToShoppingCardAppService(
             IRepository<Category, Guid> categoryRepository,
             IRepository<Coupon, Guid> couponRepository,
            IRepository<ProductGroup, Guid> productgroupRepository,
            IRepository<ItemPrice, Guid> itempricesRepository,
            IRepository<Item, Guid> itemRepository,
            BrowserInfomation browserInfomation,            
            CookieService cookieService,
            UserCookieManager userCookieManager,
            IItemAppService itemAppService, 
            IRepository<OrderDetailTemp, Guid> orderdetailTeamRepository) : base(orderdetailTeamRepository)
        {
            _orderdetailTeamRepository = orderdetailTeamRepository;
            _itemAppService = itemAppService;            
            _cookieService = cookieService;
            _userCookieManager = userCookieManager;           
            _browserInfomation = browserInfomation;
            _categoryRepository = categoryRepository;
            _productgroupRepository = productgroupRepository;
            _itempricesRepository = itempricesRepository;
            _itemRepository = itemRepository;
            _couponRepository = couponRepository;
        }
        public async Task<ResponseStatusCodesDto> CreateShoppingCart(CreateUpdateOrderDetailTempMin input)
        {
            try
            {
                ValidateCookie validateCookie = new ValidateCookie(
                    _browserInfomation,
                    _cookieService,
                    _userCookieManager);

                var checkCookieId = await validateCookie.IsCookieValid();

                var itemAllData = await _itemAppService.GetItems(input.ItemId);
                if (itemAllData == null)
                {
                    return new ResponseStatusCodesDto
                    {
                        StatusCode = itemAllData == null ? 700 : 800,
                        StatusMessage = "No Item Found"
                    };
                }
                //Not enought time to implement tax so am going just used a percentage
                //Not enought time to implement LineDiscount so am going just used 0
                //Not enought time to implement ListPrice and UnitPrice will be the same for now so
                double _quantity = 0;
                if (input.Quantity == 0)
                {
                    _quantity = 1;
                }

                var temCart = new CreateUpdateOrderDetailTempDto()
                {
                    ItemId = input.ItemId,
                    OrderNo = input.OrderNo,
                    CookieTrackerId = checkCookieId,
                    OrderDate = DateTime.Now,
                    TaxID = "Tax",
                    Status = "InProgress",
                    CategoryId = itemAllData.category.Id,
                    ProductGroupId = itemAllData.productGroup.Id,
                    Decription = itemAllData.allItems.Description,
                    ExchangeRate = 1,
                    Quantity = _quantity,
                    UnitPrice = itemAllData.price.SellCost,
                    ListPrice = itemAllData.price.SellCost,
                    TaxAmount = 0.15 * itemAllData.price.SellCost,
                    Shipping = itemAllData.price.ShippingCost,                    
                    LineDiscount = 0,
                    SubTotal = itemAllData.price.SellCost * _quantity,
                    LineTotal = (0.15 * itemAllData.price.SellCost) 
                    + (itemAllData.price.SellCost * _quantity) + itemAllData.price.ShippingCost

                };

                var saveTempCart = ObjectMapper.Map<CreateUpdateOrderDetailTempDto, OrderDetailTemp>(temCart);
                await _orderdetailTeamRepository.InsertAsync(saveTempCart);


                return new ResponseStatusCodesDto
                {
                    StatusCode = 800,
                    StatusMessage = "Item add to card successfull"
                };
            }
            catch (Exception ex)
            {
                return new ResponseStatusCodesDto
                {
                    StatusCode = 600,
                    StatusMessage = "An error occurred. Please check the syslog for more details."
                };
            }
            
        }
        public async Task UpdateShoppingCart(Guid detailId, double quantity, string? CoupanCode)
        {

            double _couponAmount = 0;
            int _couponUsageLimit = 0;

            var _couponData = await _couponRepository.GetListAsync(p =>
                                                     p.Code.Equals(CoupanCode)
                                                     && p.IsValidFrom <= DateTime.Now
                                                     && p.IsValidToDate >= DateTime.Now
                                                     && p.AmountUsed < p.UsageLimit);
            if (!_couponData.Any())
            {
                _couponAmount = 0;
                _couponUsageLimit = 0;
            }
            else
            {
                // ToDo index is not safe so will remove when i get time
                if (_couponData[0].CouponType.Equals("Amount"))
                {
                    _couponAmount = _couponData[0].DiscountAmount;
                }
                else
                {
                    //ToDo will not implement for this demo
                    _couponAmount = _couponData[0].DiscountAmount;
                }

                _couponUsageLimit = _couponData[0].UsageLimit;
            }



            var updateCartData = await _orderdetailTeamRepository.GetQueryableAsync();
            var updateQuery = updateCartData.SingleOrDefault(p=>p.Id==detailId);
            if (updateQuery != null)
            {
                updateQuery.Quantity = quantity;
                updateQuery.LineDiscount = _couponAmount;
                updateQuery.SubTotal = updateQuery.UnitPrice * quantity;
                updateQuery.LineTotal = (0.15 * updateQuery.UnitPrice)
                + (updateQuery.UnitPrice * quantity) + updateQuery.Shipping;

                await _orderdetailTeamRepository.UpdateAsync(updateQuery);
            }
        }
        public async Task<List<OrderDetailTempDto>> GetShoppingCartByCookieId(Guid cookieId)
        {
            List<OrderDetailTempDto> orderDetailTempDto = new List<OrderDetailTempDto>();

            var ShoppingCard = await _orderdetailTeamRepository
                .GetListAsync(p => p.CookieTrackerId == cookieId);
            if (ShoppingCard.Any())
            {
                orderDetailTempDto = ObjectMapper.Map<List<OrderDetailTemp>, List<OrderDetailTempDto>>(ShoppingCard);
            }
                       
            return orderDetailTempDto;
        }
        public async Task<OrderDetailTempDto> GetShoppingCartTotalByCookieId(Guid cookieId)
        {
            var _orderDetailTemp = await _orderdetailTeamRepository.GetListAsync();
            

            var mapOrderDetailTemp = ObjectMapper.Map<List<OrderDetailTemp>, List<OrderDetailTempDto>>(_orderDetailTemp);
            
            var orderTempData = await _orderdetailTeamRepository.GetListAsync(p=>p.CookieTrackerId == cookieId);
            var orderTempQuery = (from detail in orderTempData
                                  group detail by detail.CookieTrackerId into g
                                  select new OrderDetailTempDto
                                  {
                                      CookieTrackerId = g.Key,                                      
                                      SubTotal = g.Sum(x => x.SubTotal),
                                      LineTotal = g.Sum(x => x.LineTotal),
                                      TaxAmount = g.Sum(x => x.TaxAmount),
                                      LineDiscount = g.Sum(x => x.LineDiscount),
                                      Shipping = g.Sum(x => x.Shipping)
                                  }).FirstOrDefault();

            return orderTempQuery;
        }
        public async Task<OrderDetailTempDto> GetShoppingCartAmountByCookieId(Guid cookieId)
        {
            var _orderDetailTemp = await _orderdetailTeamRepository.GetListAsync();


            var mapOrderDetailTemp = ObjectMapper.Map<List<OrderDetailTemp>, List<OrderDetailTempDto>>(_orderDetailTemp);

            var orderTempData = await _orderdetailTeamRepository.GetListAsync(p => p.CookieTrackerId == cookieId);
            var orderTempQuery = (from detail in orderTempData
                                  group detail by detail.CookieTrackerId into g
                                  select new OrderDetailTempDto
                                  {                                     
                                      LineTotal = g.Sum(x => x.LineTotal),
                                      Shipping = g.Sum(x => x.Shipping),
                                      Quantity = g.Count()
                                  }).FirstOrDefault();

            return orderTempQuery;
        }
        public async Task<List<OrderWithItemDataDto>> GetShoppingCartForCheckOut(Guid cookieId)
        {
            var _orderDetailTemp = await _orderdetailTeamRepository.GetListAsync();

            var items = await _itemRepository.GetListAsync();           
            var productGroups = await _productgroupRepository.GetListAsync();
            var itemPrices = await _itempricesRepository.GetListAsync();


            var mapItems = ObjectMapper.Map<List<Item>, List<ItemDto>>(items);            
            var mapProductGroups = ObjectMapper.Map<List<ProductGroup>, List<ProductGroupDto>>(productGroups);
            var mapItemPrices = ObjectMapper.Map<List<ItemPrice>, List<ItemPriceDto>>(itemPrices);


            var mapOrderDetailTemp = ObjectMapper.Map<List<OrderDetailTemp>, List<OrderDetailTempDto>>(_orderDetailTemp);

            var orderTempData = await _orderdetailTeamRepository.GetListAsync(p => p.CookieTrackerId == cookieId);
            var orderTempQuery = (from detail in orderTempData                                  
                                  join productGroup in mapProductGroups on detail.ProductGroupId equals productGroup.Id
                                  join qitems in mapItems on detail.ItemId equals qitems.Id
                                  group detail by new 
                                  { 
                                    itemName = qitems.Name,
                                    qitems.Description,
                                    productGroup.Name,
                                    qitems.LongDescription,
                                    qitems.ItemNo,
                                    qitems.IconUrl,
                                    qitems.Id,
                                    detailId = detail.Id,
                                    
                                    
                                  }  into g
                                  select new OrderWithItemDataDto
                                  {   DetailId = g.Key.detailId,
                                      ItemId = g.Key.Id,
                                      ItemName = g.Key.itemName,
                                      ShortDescription = g.Key.Description,
                                      LongDescription = g.Key.LongDescription,
                                      ItemNo = g.Key.ItemNo,
                                      Group = g.Key.Name,
                                      IconUrl = g.Key.IconUrl,
                                      UnitPrice = g.Sum(x=>x.ListPrice),
                                      LineTotal = g.Sum(x => x.LineTotal),
                                      Quantity = g.Sum(x => x.Quantity),
                                      SubTotal = g.Sum(x=>x.SubTotal),
                                      Shipment = g.Sum(x => x.Shipping),
                                      TaxAmount = g.Sum(x => x.TaxAmount),

                                  }).ToList();

            return orderTempQuery;
        }


    }
}
