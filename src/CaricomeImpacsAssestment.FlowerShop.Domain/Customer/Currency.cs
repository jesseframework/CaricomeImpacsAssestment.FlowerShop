using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CaricomeImpacsAssestment.FlowerShop.Customer
{
    public class Currency : FullAuditedAggregateRoot<Guid>
    {
        public Currency() { }
        public string CurrencyCode { get; set; } = "JMD";
        public string CurrencyName { get; set; } = "Jamaican Doller";
        public decimal ExchangeRate { get; set; } = 1;
    }
}
