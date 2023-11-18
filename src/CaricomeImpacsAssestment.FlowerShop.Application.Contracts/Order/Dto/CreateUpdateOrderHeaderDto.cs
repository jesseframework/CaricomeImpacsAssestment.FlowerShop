using System;

namespace CaricomeImpacsAssestment.FlowerShop.Order.Dto
{
    public class CreateUpdateOrderHeaderDto
    {
        public string OrderNo { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CookieTrackerId { get; set; }
        public Guid CustomerAccountId { get; set; }
        public Guid BillToAddressId { get; set; }
        public Guid ShipToAddressId { get; set; }
        public double TaxAmount { get; set; }
        public double SubTotal { get; set; }
        public double TotalAmount { get; set; }
        public double TotalDue { get; set; }
        public double Shipping { get; set; }
        public double TotalDiscount { get; set; }
        public string? CouponCode { get; set; }
    }
}
