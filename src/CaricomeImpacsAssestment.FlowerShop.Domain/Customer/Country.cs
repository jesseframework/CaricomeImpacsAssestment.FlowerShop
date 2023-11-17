using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CaricomeImpacsAssestment.FlowerShop.Customer
{
    public class Country : FullAuditedAggregateRoot<Guid>
    {
        public Country() { }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
