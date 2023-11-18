using System;

namespace CaricomeImpacsAssestment.FlowerShop.Customer.Dto
{
    public class CreateUpdateCustomerAccountDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public string CustomerNo { get; set; }
        public Guid ContactId { get; set; }
        public Guid BillingAddressId { get; set; }
        public Guid ShippingAddressId { get; set; }
        public Guid CountryId { get; set; }
        public Guid CurrencyId { get; set; }
    }
}
