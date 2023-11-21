using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using CaricomeImpacsAssestment.FlowerShop.Product.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaricomeImpacsAssestment.FlowerShop.Order.Dto
{
    public class OrderTrackingDto
    {
        public AddressDto address { get; set; }
        public AddressTypeDto addressType { get; set; }
        public ContactDto contact { get; set; }
        public CountryDto country { get; set; }
        public CurrencyDto currency { get; set; }
        public CustomerAccountDto account { get; set; }
        public OrderHeaderDto orderHeader { get; set; }
        public OrderDetailDto orderDetail { get; set; }
        public ProductGroupDto group { get; set; }
        public CategoryDto category { get; set; }
        public ItemDto items { get; set; }
        public ItemPriceDto itemPrice { get; set; }
    }
}
