using System;
using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Product.Dto
{
    public class ProductGroupDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
