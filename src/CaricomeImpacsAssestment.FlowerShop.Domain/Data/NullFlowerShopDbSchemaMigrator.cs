using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CaricomeImpacsAssestment.FlowerShop.Data;

/* This is used if database provider does't define
 * IFlowerShopDbSchemaMigrator implementation.
 */
public class NullFlowerShopDbSchemaMigrator : IFlowerShopDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
