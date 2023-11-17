using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CaricomeImpacsAssestment.FlowerShop.Product
{
    public class ItemPrice : FullAuditedAggregateRoot<Guid>
    {
        public string PricingID { get; set; }
        public string PriceOrDiscount { get; set; }
        public string Description { get; set; }
        public Guid CountryId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Boolean IsActive { get; set; }
        public virtual Guid ItemId { get; set; }
        public virtual Item Item { get; set; }
        public string UserDefine1 { get; set; }
        public double BuyCost { get; set; }
        public double SellCost { get; set; }

    }
}
