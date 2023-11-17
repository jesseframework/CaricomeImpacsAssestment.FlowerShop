using Volo.Abp.Domain.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Product.Manager
{
    public class ItemsAllJoin : DomainService
    {
        public Category category { get; set; }
        public ProductGroup productGroup { get; set; }
        public ItemPrice price { get; set; }
        public Item allItems { get; set; }
    }
}
