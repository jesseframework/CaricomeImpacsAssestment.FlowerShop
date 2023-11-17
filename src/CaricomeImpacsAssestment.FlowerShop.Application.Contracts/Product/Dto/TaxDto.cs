using Volo.Abp.Application.Dtos;

namespace CaricomeImpacsAssestment.FlowerShop.Product.Dto
{
    public class TaxDto : FullAuditedEntityDto<TaxDto>
    {
        public string TaxName { get; set; }
        public string TaxCode { get; set; }
    }
}
