using System;

namespace CaricomeImpacsAssestment.FlowerShop.Customer.Dto
{
    public class CreateUpdateAddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public Guid CountryId { get; set; }
        public Guid AddressTypeId { get; set; }
    }
}
