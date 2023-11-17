using System;
using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Order.Dto
{
    public class CookieTrackerDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Domain { get; set; }
        public string Path { get; set; }
        public DateTime? Expiry { get; set; }
        public bool IsSecure { get; set; }
        public bool IsHttpOnly { get; set; }
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
    }
}
