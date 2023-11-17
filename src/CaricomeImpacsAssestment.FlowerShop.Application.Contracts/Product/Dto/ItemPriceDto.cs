using System;
using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Product.Dto
{
    public class ItemPriceDto : FullAuditedEntityDto<Guid>
    {
        public string PricingID { get; set; }
        public string PriceOrDiscount { get; set; }
        public string Description { get; set; }
        public Guid CountryId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Boolean IsActive { get; set; }
        public virtual Guid ItemId { get; set; }
        public string UserDefine1 { get; set; }
        public double BuyCost { get; set; }
        public double SellCost { get; set; }
    }
}
