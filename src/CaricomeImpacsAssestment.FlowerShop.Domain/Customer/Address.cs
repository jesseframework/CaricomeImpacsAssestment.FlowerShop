using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CaricomeImpacsAssestment.FlowerShop.Customer
{
    public class Address : FullAuditedAggregateRoot<Guid>
    {
        public Address() { }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public Guid CountryId { get; set; }
        public Guid AddressTypeId { get; set; }
    }
}
