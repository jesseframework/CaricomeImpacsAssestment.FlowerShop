using System;
using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Customer.Dto
{
    public class AddressTypeDto : FullAuditedEntityDto<Guid>
    {
        public string Type { get; set; }
    }
}
