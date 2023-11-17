using System;
using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Customer.Dto
{
    public class CountryDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
