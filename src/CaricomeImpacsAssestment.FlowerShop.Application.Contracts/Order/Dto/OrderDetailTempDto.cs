using System;
using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Order.Dto
{
    public class OrderDetailTempDto : FullAuditedEntityDto<Guid>
    {
        public string OrderNo { get; set; }
        public Guid CookieTrackerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string TaxID { get; set; }
        public string Status { get; set; }
        public Guid ItemId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ProductGroupId { get; set; }
        public string Decription { get; set; }
        public double ExchangeRate { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double ListPrice { get; set; }
        public double TaxAmount { get; set; }
        public double LineDiscount { get; set; }
        public double Shipping { get; set; }
        public double SubTotal { get; set; }
        public double LineTotal { get; set; }
    }
}
