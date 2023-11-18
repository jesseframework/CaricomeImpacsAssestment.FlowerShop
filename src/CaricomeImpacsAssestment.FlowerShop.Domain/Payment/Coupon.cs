using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace CaricomeImpacsAssestment.FlowerShop.Payment
{
    public class Coupon : FullAuditedAggregateRoot<Guid>
    {
        public Coupon() { }
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
