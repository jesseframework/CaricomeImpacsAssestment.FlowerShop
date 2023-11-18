using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Payment.Dto
{
    public class CouponDto: FullAuditedEntityDto<Guid>
    {
        public string CouponName { get; set; }
        public string CouponType { get; set; } //Amount,Percentage
        public string Code { get; set; }
        public double DiscountAmount { get; set; }
        public double PercentageAmount { get; set; }
        public DateTime IsValidFrom { get; set; }
        public DateTime IsValidToDate { get; set; }
        public int UsageLimit { get; set; }
        public int AmountUsed { get; set; }
        public bool IsUsed { get; set; }
    }
}
