using CaricomeImpacsAssestment.FlowerShop.AppLogger;
using CaricomeImpacsAssestment.FlowerShop.Customer;
using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using CaricomeImpacsAssestment.FlowerShop.Order.Manager;
using CaricomeImpacsAssestment.FlowerShop.Payment;
using CaricomeImpacsAssestment.FlowerShop.Payment.Dto;
using CaricomeImpacsAssestment.FlowerShop.Product;
using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using CaricomeImpacsAssestment.FlowerShop.Settings;
using Microsoft.AspNetCore.Http;
using Nito.AsyncEx.Synchronous;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace CaricomeImpacsAssestment.FlowerShop.Order
{
    public class CompleteAndCheckOutAppService : CrudAppService<
        OrderHeader,
        OrderHeaderDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateOrderHeaderDto>,
        IOrderHeaderAppService
    {
        private readonly IRepository<CustomerAccount, Guid> _customerRepository;
        private readonly IRepository<Country, Guid> _countryRepository;
        private readonly IRepository<Currency, Guid> _currencyRepository;
        private readonly IRepository<Contact, Guid> _contactRepository;
        private readonly IRepository<Address, Guid> _addressRepository;
        private readonly IRepository<AddressType, Guid> _addressTypeRepository;
        private readonly IRepository<OrderHeader, Guid> _orderHeaderRepository;
        private readonly IRepository<Coupon, Guid> _couponRepository;
        private readonly IRepository<OrderDetail, Guid> _orderDetailRepository;
        private readonly IRepository<OrderPayment, Guid> _orderPaymentRepository;
        private readonly IItemAppService _itemAppService;
        private readonly ICustomerAccountAppService _customerAccountAppService;        
        private readonly IOrderDetailTempAppService _orderDetailTempAppService;       
        private readonly UserCookieManager _userCookieManager;
        private readonly BrowserInfomation _browserInfomation;
        private readonly CookieService _cookieService;
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<ProductGroup, Guid> _productgroupRepository;
        private readonly IRepository<ItemPrice, Guid> _itempricesRepository;
        private readonly IRepository<Item, Guid> _itemRepository;
        private readonly IRepository<CookieTracker, Guid> _cookietrackingRepository;
        public CompleteAndCheckOutAppService(
            BrowserInfomation browserInfomation,
            UserCookieManager userCookieManager,
            CookieService cookieService,
            IItemAppService itemAppService,
            IRepository<OrderHeader, Guid> orderHeaderRepository,
            ICustomerAccountAppService customerAccountAppService,            
            IOrderDetailTempAppService orderDetailTempAppService,
            IRepository<OrderPayment, Guid> orderPaymentRepository,
            IRepository<Coupon, Guid> couponRepository,
            IRepository<Country, Guid> countryRepository,
            IRepository<Currency, Guid> currencyRepository,
            IRepository<CustomerAccount, Guid> customerRepository,
            IRepository<Contact, Guid> contactRepository,
            IRepository<Address, Guid> addressRepository,
            IRepository<AddressType, Guid> addressTypeRepository,
            IRepository<Category, Guid> categoryRepository,
            IRepository<ProductGroup, Guid> productgroupRepository,
            IRepository<ItemPrice, Guid> itempricesRepository,
            IRepository<Item, Guid> itemRepository,
            IRepository<OrderDetail, Guid> orderDetailRepository,
            IRepository<CookieTracker, Guid> cookietrackingRepository) : base(orderHeaderRepository) 
        {
            _orderHeaderRepository = orderHeaderRepository;
            _itemAppService = itemAppService;
            _orderDetailRepository = orderDetailRepository;
            _orderPaymentRepository = orderPaymentRepository;
            _orderDetailTempAppService = orderDetailTempAppService;
            _customerAccountAppService = customerAccountAppService;            
            _userCookieManager = userCookieManager;
            _browserInfomation = browserInfomation;
            _cookieService = cookieService;
            _couponRepository = couponRepository;
            _addressRepository = addressRepository;
            _customerRepository = customerRepository;
            _contactRepository = contactRepository;
            _addressTypeRepository = addressTypeRepository;
            _countryRepository = countryRepository;
            _currencyRepository = currencyRepository;
            _categoryRepository = categoryRepository;
            _productgroupRepository = productgroupRepository;
            _itempricesRepository = itempricesRepository;
            _itemRepository = itemRepository;
            _cookietrackingRepository = cookietrackingRepository;
        }
        public async Task<ResponseStatusCodesDto> CreateCompleteOrderAsync(CreateUpdateOrderHeaderDtoMin input)
        {
            ValidateCookie validateCookie = new ValidateCookie(
                    _browserInfomation,
                    _cookieService,
                    _userCookieManager);
            
            List<CreateUpdateOrderDetailDto> orderdetailIst = new List<CreateUpdateOrderDetailDto>();
            var customerAccountData = await _customerAccountAppService.GetCustomeWithReference();
            if (customerAccountData == null)
            {
                return new ResponseStatusCodesDto
                {
                    StatusCode = customerAccountData == null ? 700 : 800,
                    StatusMessage = "No Customer Found"
                };
            }

            var customerBillingAddress = await _customerAccountAppService.GetCustomeBillingAddressWithReference();
            if (customerBillingAddress == null)
            {
                return new ResponseStatusCodesDto
                {
                    StatusCode = customerBillingAddress == null ? 700 : 800,
                    StatusMessage = "No Billing Address"
                };
            }

            var customerShipToAddress = await _customerAccountAppService.GetCustomeShippingAddressWithReference();
            if (customerShipToAddress == null)
            {
                return new ResponseStatusCodesDto
                {
                    StatusCode = customerShipToAddress == null ? 700 : 800,
                    StatusMessage = "No Shipping Address"
                };
            }


            var checkCookieId = await validateCookie.IsCookieValid();
            var _orderNo = await _cookietrackingRepository.GetAsync(checkCookieId);
            if(_orderNo == null)
            {
                return new ResponseStatusCodesDto
                {
                    StatusCode = _orderNo == null ? 700 : 800,
                    StatusMessage = "No Order No"
                };
            }

            var orderDetailTemp = await _orderDetailTempAppService.GetShoppingCartByCookieId(checkCookieId);           
            if (!orderDetailTemp.Any())
            {
                return new ResponseStatusCodesDto
                {
                    StatusCode = orderDetailTemp == null ? 700 : 800,
                    StatusMessage = "No Shopping Cart Found"
                };
            }

            var shoppingCartTotal = await _orderDetailTempAppService.GetShoppingCartTotalByCookieId(checkCookieId);
            if (shoppingCartTotal == null)
            {
                return new ResponseStatusCodesDto
                {
                    StatusCode = shoppingCartTotal == null ? 700 : 800,
                    StatusMessage = "No Order Total Found"
                };
            }

            double _couponAmount = 0;           
            int _couponUsageLimit = 0;

            var _couponData = await _couponRepository.GetListAsync(p => 
                                                     p.Code.Equals(input.YourCouponCode)
                                                     && p.IsValidFrom <= DateTime.Now
                                                     && p.IsValidToDate >= DateTime.Now                                                     
                                                     && p.AmountUsed < p.UsageLimit);
            if(!_couponData.Any())
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

            double _totalDueBeforeCoupon = shoppingCartTotal.LineTotal
                + shoppingCartTotal.TaxAmount
                + shoppingCartTotal.Shipping;

            double _totalDueAfterCoupon = _totalDueBeforeCoupon - _couponAmount;

            //ToDo If you have more time validate calculation
            var saveOrderHeader = new CreateUpdateOrderHeaderDto()
            {
                
                CookieTrackerId = checkCookieId,
                OrderDate = DateTime.Now,
                OrderNo = _orderNo.OrderNo,
                CustomerAccountId = customerAccountData.account.Id,
                BillToAddressId = customerBillingAddress.address.Id,
                ShipToAddressId = customerShipToAddress.address.Id,
                SubTotal = shoppingCartTotal.SubTotal,
                Status = "Draft",
                TaxAmount = shoppingCartTotal.TaxAmount,
                TotalAmount = shoppingCartTotal.LineTotal,
                Shipping = shoppingCartTotal.Shipping,
                TotalDiscount = shoppingCartTotal.LineDiscount + _couponAmount,
                CouponCode = input.YourCouponCode,
                TotalDue = _totalDueAfterCoupon

            };

            var saveCartHeader = ObjectMapper.Map<CreateUpdateOrderHeaderDto, OrderHeader>(saveOrderHeader);
            
            var IsHeaderInDb = await _orderHeaderRepository.GetListAsync(p=>p.CookieTrackerId == checkCookieId);
            if (!IsHeaderInDb.Any())
            {
                await _orderHeaderRepository.InsertAsync(saveCartHeader);
            }
            else
            {
                var InHeaderInDb = await _orderHeaderRepository.GetAsync(p => p.CookieTrackerId == checkCookieId);
                if (InHeaderInDb != null)
                {
                    await _orderHeaderRepository.UpdateAsync(InHeaderInDb);
                }
               
            }

            

            foreach (var detail in orderDetailTemp)
            {
                var saveOrderDetail = new CreateUpdateOrderDetailDto()
                {
                    ItemId = detail.ItemId,
                    OrderNo = detail.OrderNo,
                    CookieTrackerId = detail.CookieTrackerId,
                    OrderDate = DateTime.Now,
                    TaxID = detail.TaxID,
                    Status = "Draft",
                    CategoryId = detail.CategoryId,
                    ProductGroupId = detail.ProductGroupId,
                    Decription = detail.Decription,
                    ExchangeRate = detail.ExchangeRate,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice,
                    ListPrice = detail.ListPrice,
                    TaxAmount = detail.TaxAmount,
                    LineDiscount = detail.LineDiscount,
                    SubTotal = detail.SubTotal,
                    LineTotal = detail.LineTotal,
                    OrderHeaderId = saveCartHeader.Id

                };

               

                //This is not good bu am out of time so here we go
                var IsDetailInDb = await _orderDetailRepository.GetListAsync(p => p.CookieTrackerId == checkCookieId);
                if (!IsDetailInDb.Any())
                {
                   
                    orderdetailIst.Add(saveOrderDetail);
                }
                else
                {


                    foreach(var data in IsDetailInDb)
                    {
                        var updatenow = await _orderDetailRepository.GetAsync(data.Id);
                        if(updatenow != null)
                        {
                            updatenow.Quantity = data.Quantity;
                            updatenow.UnitPrice = data.UnitPrice;
                            updatenow.ListPrice = data.ListPrice;
                            updatenow.TaxAmount = data.TaxAmount;
                            updatenow.LineDiscount = data.LineDiscount;
                            updatenow.SubTotal = data.SubTotal;
                            updatenow.LineTotal = data.LineTotal;
                        }

                        await _orderDetailRepository.UpdateAsync(data);
                       
                    }

                }

            }

            var saveCartDetail = ObjectMapper.Map<List<CreateUpdateOrderDetailDto>, List<OrderDetail>>(orderdetailIst);

            await _orderDetailRepository.InsertManyAsync(saveCartDetail);


            return new ResponseStatusCodesDto
            {
                StatusCode = 800,
                StatusMessage = "Order complete successfull"
            };
        }

        public async Task<ResponseStatusCodesDto> CreateOrderConfirmation(string tenderType, string paymentNo)
        {
            ValidateCookie validateCookie = new ValidateCookie(
                    _browserInfomation,
                    _cookieService,
                    _userCookieManager);
            var checkCookieId = await validateCookie.IsCookieValid();

            var _header = await _orderHeaderRepository.SingleOrDefaultAsync(p => p.CookieTrackerId == checkCookieId);
            if (_header == null)
            {
                return new ResponseStatusCodesDto
                {
                    StatusCode = _header == null ? 700 : 800,
                    StatusMessage = "No Header Found"
                };
            }
            else
            {
                _header.Status = "Success";
                await _orderHeaderRepository.UpdateAsync(_header);
            }

            var _detail = await _orderDetailRepository.GetListAsync(p => p.CookieTrackerId == checkCookieId);
            if (_detail.Any())
            {
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                bool ChangeMade = false;
                foreach (var item in _detail)
                {
                    var _detailUpdate = await _orderDetailRepository.SingleOrDefaultAsync(p => p.Id == item.Id);
                    if (_detailUpdate != null)
                    {
                        _detailUpdate.Status = "Success";
                        ChangeMade = true;
                    }

                    orderDetails.Add(_detailUpdate);

                }
                if (ChangeMade == true)
                    await _orderDetailRepository.UpdateManyAsync(orderDetails);
            }
            else
            {
                _header.Status = "Success";
                await _orderHeaderRepository.UpdateAsync(_header);
            }
            var savePayment = new CreateUpdateOrderPaymentDto()
            {
                OrderNo = _header.OrderNo,
                PaymentDate = DateTime.Now,
                Status = "Success",
                CustomerAccountId = _header.CustomerAccountId,
                OrderHeaderId = _header.Id,
                OrderAmount = _header.TotalAmount,
                AmountPay = _header.TotalAmount,
                OrderBalance = _header.TotalAmount - _header.TotalDue,
                TenderType = tenderType,
                PaymentNo = paymentNo //Come from payment gateway
            };

            var saveCartPayment = ObjectMapper.Map<CreateUpdateOrderPaymentDto, OrderPayment>(savePayment);
            await _orderPaymentRepository.InsertAsync(saveCartPayment);

            return new ResponseStatusCodesDto
            {
                StatusCode = 800,
                StatusMessage = "Payment complete successfull"
            };


        }
        public async Task<OrderHeaderDto> GetOrderConfimTotalByCookieId(Guid cookieId)
        {
            
            //ToDo Add paramater to this before going to the db. I wish i have more time
            var _orderHeader = await _orderHeaderRepository.GetListAsync();


            var mapOrderHeader = ObjectMapper.Map<List<OrderHeader>, List<OrderHeaderDto>>(_orderHeader);

            var orderHeaderData = await _orderHeaderRepository.GetListAsync(p => p.CookieTrackerId == cookieId
                                                                            && p.Status.Equals("Draft"));
            var orderHeaderQuery = (from header in orderHeaderData
                                    group header by new 
                                    {
                                        header.CookieTrackerId,
                                        header.CouponCode
                                        

                                    }  into g
                                  select new OrderHeaderDto
                                  {
                                      CouponCode = g.Key.CouponCode,
                                      SubTotal = g.Sum(x => x.SubTotal),
                                      TotalDue = g.Sum(x => x.TotalDue),
                                      Shipping = g.Sum(x => x.Shipping),
                                      TaxAmount = g.Sum(x => x.TaxAmount),
                                      TotalDiscount = g.Sum(x => x.TotalDiscount),
                                      
                                      
                                  }).FirstOrDefault();

            return orderHeaderQuery;
        }

        public async Task<List<OrderTrackingDto>> GetAllOrderWithCustomer()
        {
            //Am aware that this section of my code could have db perfromance issue if am dealing with large data.
            var _address = await _addressRepository.GetListAsync();
            var _contact = await _contactRepository.GetListAsync();
            var _account = await _customerRepository.GetListAsync();
            var _country = await _countryRepository.GetListAsync();
            var _currency = await _currencyRepository.GetListAsync();
            var _orderheader = await _orderHeaderRepository.GetListAsync();
            var _orderDetail = await _orderDetailRepository.GetListAsync();
            var items = await _itemRepository.GetListAsync();
            var categories = await _categoryRepository.GetListAsync();
            var productGroups = await _productgroupRepository.GetListAsync();
            var itemPrices = await _itempricesRepository.GetListAsync();

            var mapAddress = ObjectMapper.Map<List<Address>, List<AddressDto>>(_address);
            var mapContact = ObjectMapper.Map<List<Contact>, List<ContactDto>>(_contact);
            var mapAccount = ObjectMapper.Map<List<CustomerAccount>, List<CustomerAccountDto>>(_account);
            var mapCurrency = ObjectMapper.Map<List<Currency>, List<CurrencyDto>>(_currency);
            var mapCountry = ObjectMapper.Map<List<Country>, List<CountryDto>>(_country);
            var mapOrderHeader = ObjectMapper.Map<List<OrderHeader>, List<OrderHeaderDto>>(_orderheader);
            var mapOrderDetail = ObjectMapper.Map<List<OrderDetail>, List<OrderDetailDto>>(_orderDetail);
            var mapItems = ObjectMapper.Map<List<Item>, List<ItemDto>>(items);
            var mapCategories = ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories);
            var mapProductGroups = ObjectMapper.Map<List<ProductGroup>, List<ProductGroupDto>>(productGroups);
            var mapItemPrices = ObjectMapper.Map<List<ItemPrice>, List<ItemPriceDto>>(itemPrices);


            var queryResult = (from account in mapAccount
                               join address in mapAddress on account.BillingAddressId equals address.Id
                               join contact in mapContact on account.ContactId equals contact.Id
                               join country in mapCountry on account.CountryId equals country.Id
                               join currency in mapCurrency on account.CurrencyId equals currency.Id
                               join header in mapOrderHeader on account.Id equals header.CustomerAccountId
                               join detail in mapOrderDetail on header.Id equals detail.Id
                               join item in mapItems on detail.ItemId equals item.Id
                               join category in mapCategories on item.Id equals category.Id
                               join product in mapProductGroups on item.Id equals product.Id
                               join price in mapItemPrices on item.Id equals price.Id
                               
                               where
                               account.IsDeleted == false                               
                               select new OrderTrackingDto
                               {
                                   account = account,
                                   contact = contact,
                                   address = address,
                                   country = country,
                                   currency = currency,
                                   orderDetail = detail,
                                   orderHeader = header


                               }).ToList();

            return queryResult;


        }
        public async Task<List<OrderWithItemDataDto>> GetOrderAllOrderSum()
        {

            var orderHeaders = await _orderHeaderRepository.GetListAsync();
            var customers = await _customerRepository.GetListAsync();
            var addresses = await _addressRepository.GetListAsync();
            var countries = await _countryRepository.GetListAsync();
            var orderDetails = await _orderDetailRepository.GetListAsync();
           
            var customerDtos = ObjectMapper.Map<List<CustomerAccount>, List<CustomerAccountDto>>(customers);
            var addressDtos = ObjectMapper.Map<List<Address>, List<AddressDto>>(addresses);
            var countryDtos = ObjectMapper.Map<List<Country>, List<CountryDto>>(countries);
            var orderDetailDtos = ObjectMapper.Map<List<OrderDetail>, List<OrderDetailDto>>(orderDetails);

            
            var orderHeaderQuery = (from header in orderHeaders
                                    join detail in orderDetailDtos on header.Id equals detail.OrderHeaderId
                                    join account in customerDtos on header.CustomerAccountId equals account.Id
                                    join address in addressDtos on header.BillToAddressId equals address.Id
                                    join country in countryDtos on account.CountryId equals country.Id
                                    group new { header, detail, address, account, country } by new
                                    {
                                        header.OrderNo,
                                        header.OrderDate,
                                        header.Status,
                                        AccountName = account.Name,
                                        address.Street,
                                        address.City,
                                        CountryName = country.Name
                                    } into g
                                    select new OrderWithItemDataDto
                                    {
                                        Street = g.Key.Street,
                                        City = g.Key.City,
                                        CountryName = g.Key.CountryName,
                                        Name = g.Key.AccountName,
                                        OrderNo = g.Key.OrderNo,
                                        OrderDate = g.Key.OrderDate,
                                        Status = g.Key.Status,
                                        Quantity = g.Sum(x => x.detail.Quantity), 
                                        Shipment = g.Sum(x => x.header.Shipping), 
                                        SubTotal = g.Sum(x => x.header.SubTotal), 
                                        TotalDue = g.Sum(x => x.header.TotalDue), 
                                        TaxAmount = g.Sum(x => x.header.TaxAmount), 
                                        TotalDiscount = g.Sum(x => x.header.TotalDiscount) 
                                    }).ToList();

            return orderHeaderQuery;
        }
        public async Task<OrderHeaderDto> GetOrderHeaderByOrderNo(string orderNo)
        {
            OrderHeaderDto orderHeader= new OrderHeaderDto();
            var order = await _orderHeaderRepository.GetAsync(p=>p.OrderNo.Equals(orderNo));
            if (order != null)
            {
                orderHeader = ObjectMapper.Map<OrderHeader, OrderHeaderDto>(order);
            }

            return orderHeader;
        }

        public async Task<List<OrderWithItemDataDto>> GetOrderAllOrderWithItems(Guid Id)
        {

            var orderHeaders = await _orderHeaderRepository.GetListAsync();
            var customers = await _customerRepository.GetListAsync();
            var addresses = await _addressRepository.GetListAsync();
            var countries = await _countryRepository.GetListAsync();
            var orderDetails = await _orderDetailRepository.GetListAsync();
            var items = await _itemRepository.GetListAsync();
            var categories = await _categoryRepository.GetListAsync();
            var productGroups = await _productgroupRepository.GetListAsync();

            var customerDtos = ObjectMapper.Map<List<CustomerAccount>, List<CustomerAccountDto>>(customers);
            var addressDtos = ObjectMapper.Map<List<Address>, List<AddressDto>>(addresses);
            var countryDtos = ObjectMapper.Map<List<Country>, List<CountryDto>>(countries);
            var orderDetailDtos = ObjectMapper.Map<List<OrderDetail>, List<OrderDetailDto>>(orderDetails);
            var mapItems = ObjectMapper.Map<List<Item>, List<ItemDto>>(items);
            var mapCategories = ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories);
            var mapProductGroups = ObjectMapper.Map<List<ProductGroup>, List<ProductGroupDto>>(productGroups);


            var orderHeaderQuery = (from header in orderHeaders
                                    join detail in orderDetailDtos on header.Id equals detail.OrderHeaderId
                                    join account in customerDtos on header.CustomerAccountId equals account.Id
                                    join item in mapItems on detail.ItemId equals item.Id
                                    join productgroup in mapProductGroups on detail.ProductGroupId equals productgroup.Id
                                    join category in mapCategories on detail.CategoryId equals category.Id
                                    where(header.Id == Id)
                                    select new OrderWithItemDataDto
                                    {
                                        ItemNo = item.ItemNo,
                                        Name = account.Name, 
                                        ItemId = item.Id,
                                        DetailId = detail.Id,
                                        ItemName = item.Name,
                                        ShortDescription = item.Description,
                                        LongDescription = item.LongDescription,
                                        IconUrl = item.IconUrl,
                                        Group = productgroup.Name, 
                                        UnitPrice = detail.UnitPrice,
                                        LineTotal = detail.LineTotal, 
                                        SubTotal = header.SubTotal,
                                        Shipment = header.Shipping,
                                        Quantity = detail.Quantity,
                                        TaxAmount = header.TaxAmount,
                                        TotalDue = header.TotalDue,
                                        OrderNo = header.OrderNo,
                                        Status = header.Status,
                                        OrderDate = header.OrderDate

                                    }).ToList();

            return orderHeaderQuery;
        }
        public async Task UpdateOrderStatus(string orderStatus,Guid Id)
        {
            var updateOrder = await _orderHeaderRepository.GetAsync(Id);
            if (updateOrder != null)
            {
                updateOrder.Status = orderStatus;
                await _orderHeaderRepository.UpdateAsync(updateOrder);
            }
        }
    }
}
