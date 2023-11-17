using System;
using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Customer.Dto
{
    public class CurrencyDto : FullAuditedEntityDto<Guid>
    {
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}
