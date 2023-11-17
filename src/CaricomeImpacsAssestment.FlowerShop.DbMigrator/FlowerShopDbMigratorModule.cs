using CaricomeImpacsAssestment.FlowerShop.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace CaricomeImpacsAssestment.FlowerShop.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(FlowerShopEntityFrameworkCoreModule),
    typeof(FlowerShopApplicationContractsModule)
    )]
public class FlowerShopDbMigratorModule : AbpModule
{
}
