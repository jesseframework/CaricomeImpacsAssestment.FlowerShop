using AutoMapper;
using CaricomeImpacsAssestment.FlowerShop.Customer;
using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using CaricomeImpacsAssestment.FlowerShop.Order;
using CaricomeImpacsAssestment.FlowerShop.Order.Dto;
using CaricomeImpacsAssestment.FlowerShop.Payment;
using CaricomeImpacsAssestment.FlowerShop.Payment.Dto;
using CaricomeImpacsAssestment.FlowerShop.Product;
using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using CaricomeImpacsAssestment.FlowerShop.Setting.Dto;
using CaricomeImpacsAssestment.FlowerShop.Settings;

namespace CaricomeImpacsAssestment.FlowerShop;

public class FlowerShopApplicationAutoMapperProfile : Profile
{
    public FlowerShopApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<CustomerAccount, CustomerAccountDto>();
        CreateMap<CreateUpdateCustomerAccountDto, CustomerAccount>();

        CreateMap<Coupon, CouponDto>();
        CreateMap<CreateUpdateCouponDto, Coupon>();

        CreateMap<Currency, CurrencyDto>();
        CreateMap<CreateUpdateCurrencyDto, Currency>();

        CreateMap<Country, CountryDto>();
        CreateMap<CreateUpdateCountryDto, Country>();

        CreateMap<Contact, ContactDto>();
        CreateMap<CreateUpdateContactDto, Contact>();

        CreateMap<AddressType, AddressTypeDto>();
        CreateMap<CreateUpdateAddressTypeDto, AddressType>();

        CreateMap<Address, AddressDto>();
        CreateMap<CreateUpdateAddressDto, Address>();


        CreateMap<OrderDetail, OrderDetailDto>();
        CreateMap<CreateUpdateOrderDetailDto, OrderDetail>();

        CreateMap<OrderDetailTemp, OrderDetailTempDto>();
        CreateMap<CreateUpdateOrderDetailTempDto, OrderDetailTemp>();

        CreateMap<OrderHeader, OrderHeaderDto>();
        CreateMap<CreateUpdateOrderHeaderDto, OrderHeader>();


        CreateMap<OrderPayment, OrderPaymentDto>();
        CreateMap<CreateUpdateOrderPaymentDto, OrderPayment>();

        CreateMap<CustomerAccount, CustomerAccountDto>();
        CreateMap<CreateUpdateCustomerAccountDto, CustomerAccount>();


        CreateMap<CookieTracker, CookieTrackerDto>();
        CreateMap<CreateUpdateCookieTrackerDto, CookieTracker>();

        CreateMap<Category, CategoryDto>();
        CreateMap<CreateUpdateCategoryDto, Category>();

        CreateMap<Item, ItemDto>();
        CreateMap<CreateUpdateItemDto, Item>();

        CreateMap<ItemPrice, ItemPriceDto>();
        CreateMap<CreateUpdateItemPriceDto, ItemPrice>();

        CreateMap<ProductGroup, ProductGroupDto>();
        CreateMap<CreateUpdateProductGroupDto, ProductGroup>();

        CreateMap<Tax, TaxDto>();
        CreateMap<CreateUpdateTaxDto, Tax>();

        CreateMap<SerialNumber, SerialNumberDto>();
        CreateMap<CreateUpdateSerialNumberDto, SerialNumber>();
    }
}
