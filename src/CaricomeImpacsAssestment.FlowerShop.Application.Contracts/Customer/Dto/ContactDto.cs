using System;
using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Customer.Dto
{
    public class ContactDto : FullAuditedEntityDto<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
