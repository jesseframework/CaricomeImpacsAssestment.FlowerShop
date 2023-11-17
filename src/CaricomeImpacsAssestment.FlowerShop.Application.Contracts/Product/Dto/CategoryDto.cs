using System;
using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Product.Dto
{
    public class CategoryDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string Route { get; set; }
    }
}
