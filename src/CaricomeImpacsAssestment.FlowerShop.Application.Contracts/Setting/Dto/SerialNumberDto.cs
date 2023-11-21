using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Setting.Dto
{
    public class SerialNumberDto : AuditedEntityDto<Guid>
    {
        public int LeadingZero { get; set; }
        public bool AddLedingZero { get; set; }
        public bool IncludePrefix { get; set; }
        public bool UserAccountNo { get; set; }
        public bool UsePartnerCode { get; set; }
        public bool UserAutoNo { get; set; }
        public bool IncludeJulianDate { get; set; }
        public string ConfigPrefix { get; set; }
        public int EndingNumber { get; set; }
        public string Type { get; set; }
    }
}
