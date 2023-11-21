using System;
using System.Collections.Generic;
using System.Text;

namespace CaricomeImpacsAssestment.FlowerShop.Setting.Dto
{
    public class SerialNumberConfigurationDto
    {

        public string Type { get; set; }
        public bool IncludePrefix { get; set; }
        public bool IncludeJulianDate { get; set; }
        public bool UserAccountNo { get; set; }
        public string AccountNo { get; set; }
        public bool PartnerCode { get; set; }
        public string UsePartnerCode { get; set; }
        public bool UserAutoNo { get; set; }
        public string AutoNo { get; set; }
        public int EndingNumber { get; set; }
        public int LeadingZero { get; set; }
        public bool AddLedingZero { get; set; }
    }
}
