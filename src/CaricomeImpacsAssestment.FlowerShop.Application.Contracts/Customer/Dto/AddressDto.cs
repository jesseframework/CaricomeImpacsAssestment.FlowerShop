using System;
using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Customer.Dto
{
    public class AddressDto : FullAuditedEntityDto<Guid>
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public Guid CountryId { get; set; }
        public Guid AddressTypeId { get; set; }
    }
}
