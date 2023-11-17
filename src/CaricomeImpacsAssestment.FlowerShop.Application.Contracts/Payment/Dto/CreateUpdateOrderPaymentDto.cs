using System;

namespace CaricomeImpacsAssestment.FlowerShop.Payment.Dto
{
    public class CreateUpdateOrderPaymentDto
    {
        public string OrderNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid CustomerAccountId { get; set; }
        public Guid OrderHeaderId { get; set; }
        public double OrderAmount { get; set; }
        public double AmountPay { get; set; }
        public double OrderBalance { get; set; }
        public string TenderType { get; set; }
        public string PaymentNo { get; set; }
    }
}
