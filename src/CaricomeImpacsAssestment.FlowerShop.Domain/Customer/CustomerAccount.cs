using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CaricomeImpacsAssestment.FlowerShop.Customer
{
    public class CustomerAccount : FullAuditedAggregateRoot<Guid>
    {
        public CustomerAccount() { }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string CustomerNo { get; set; }
        public Guid ContactId { get; set; }
        public Guid BillingAddressId { get; set; }
        public Guid ShippingAddressId { get; set; }
        public Guid CountryId { get; set; }
        public Guid CurrencyId { get; set; }


    }
}
