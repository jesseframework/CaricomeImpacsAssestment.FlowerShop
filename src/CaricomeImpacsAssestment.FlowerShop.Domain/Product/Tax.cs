using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CaricomeImpacsAssestment.FlowerShop.Product
{
    public class Tax : FullAuditedAggregateRoot<Guid>
    {
        public string TaxName { get; set; }
        public string TaxCode { get; set; }

    }
}
