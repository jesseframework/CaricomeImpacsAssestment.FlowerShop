using CaricomeImpacsAssestment.FlowerShop.AppLogger;
using CaricomeImpacsAssestment.FlowerShop.Customer;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using CaricomeImpacsAssestment.FlowerShop.Order.Manager;
using CaricomeImpacsAssestment.FlowerShop.Product;
using Microsoft.AspNetCore.Http;
using System;
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
        private readonly UserCookieManager _userCookieManager;        
        private readonly BrowserInfomation _browserInfomation;
        public AddToShoppingCardAppService(
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
        }
        public async Task<ResponseStatusCodesDto> CreateShoppingCart(CreateUpdateOrderDetailTempDto input)
        {
            try
            {
                Guid cookieId = Guid.NewGuid();
                var cookieValue = _cookieService.GetBrowserCookie("Browser_Cart_Session");
                if (!string.IsNullOrEmpty(cookieValue))
                {
                    var (Domain, Path) = _browserInfomation.ProcessRequest();
                    if (!string.IsNullOrEmpty(Domain) && !string.IsNullOrEmpty(Path))
                    {
                        cookieId = await _userCookieManager.SaveCookieAsync(cookieValue, Domain, Path);
                    }

                }

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
                var temCart = new CreateUpdateOrderDetailTempDto()
                {
                    ItemId = input.ItemId,
                    OrderNo = input.OrderNo,
                    CookieTrackerId = cookieId,
                    OrderDate = DateTime.Now,
                    TaxID = "Tax",
                    Status = "InProgress",
                    CategoryId = itemAllData.category.Id,
                    ProductGroupId = itemAllData.productGroup.Id,
                    Decription = itemAllData.allItems.Description,
                    ExchangeRate = 1,
                    Quantity = input.Quantity,
                    UnitPrice = itemAllData.price.SellCost,
                    ListPrice = itemAllData.price.SellCost,
                    TaxAmount = 0.15 * itemAllData.price.SellCost,
                    LineDiscount = 0,
                    SubTotal = itemAllData.price.SellCost * input.Quantity,
                    LineTotal = (0.15 * itemAllData.price.SellCost) + itemAllData.price.SellCost * input.Quantity

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
    }
}
