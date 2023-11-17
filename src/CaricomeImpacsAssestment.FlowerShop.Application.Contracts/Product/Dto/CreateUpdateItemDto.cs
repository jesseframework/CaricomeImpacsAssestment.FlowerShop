using System;

namespace CaricomeImpacsAssestment.FlowerShop.Product.Dto
{
    public class CreateUpdateItemDto
    {
        public string ItemNo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ProductGroupId { get; set; }
        public string Parent { get; set; }
        public string LongDescription { get; set; }
        public Boolean IsActive { get; set; }
        public string TaxID { get; set; }
        public string IconUrl { get; set; }
        public Boolean IsFeatureProduct { get; set; }
        public string MarketingImagesUrl { get; set; }
        public string ItemImagesUrl1 { get; set; }
        public string ItemImagesUrl2 { get; set; }
        public string ItemImagesUrl3 { get; set; }
    }
}
