using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CaricomeImpacsAssestment.FlowerShop.Customer
{
    public class AddressType : FullAuditedAggregateRoot<Guid>
    {
        public AddressType() { }
        public string Type { get; set; }
    }
}
