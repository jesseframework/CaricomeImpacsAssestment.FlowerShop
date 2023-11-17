using CaricomeImpacsAssestment.FlowerShop.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CaricomeImpacsAssestment.FlowerShop;

[DependsOn(
    typeof(FlowerShopEntityFrameworkCoreTestModule)
    )]
public class FlowerShopDomainTestModule : AbpModule
{

}
