using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace CaricomeImpacsAssestment.FlowerShop.Settings
{
    public class SerialNumber : FullAuditedAggregateRoot<Guid>
    {
        public int LeadingZero { get; set; }
        public Boolean AddLedingZero { get; set; }
        public Boolean IncludePrefix { get; set; }
        public Boolean UserAccountNo { get; set; }
        public Boolean UsePartnerCode { get; set; }
        public Boolean UserAutoNo { get; set; }
        public Boolean IncludeJulianDate { get; set; }
        public string ConfigPrefix { get; set; }
        public int EndingNumber { get; set; }
        public string Type { get; set; }
    }
}
