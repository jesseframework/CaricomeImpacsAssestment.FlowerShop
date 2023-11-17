using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CaricomeImpacsAssestment.FlowerShop.Product
{
    public class Category : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string Route { get; set; }
    }
}
