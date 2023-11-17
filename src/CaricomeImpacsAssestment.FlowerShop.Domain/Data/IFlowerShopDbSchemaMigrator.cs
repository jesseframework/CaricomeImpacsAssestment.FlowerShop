using System.Threading.Tasks;

namespace CaricomeImpacsAssestment.FlowerShop.Data;

public interface IFlowerShopDbSchemaMigrator
{
    Task MigrateAsync();
}
