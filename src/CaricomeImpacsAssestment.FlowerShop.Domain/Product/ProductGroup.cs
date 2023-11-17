using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CaricomeImpacsAssestment.FlowerShop.Product
{
    public class ProductGroup : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
    }
}
