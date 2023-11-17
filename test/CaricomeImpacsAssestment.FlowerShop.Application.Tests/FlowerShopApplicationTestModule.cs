using Volo.Abp.Modularity;

namespace CaricomeImpacsAssestment.FlowerShop;

[DependsOn(
    typeof(FlowerShopApplicationModule),
    typeof(FlowerShopDomainTestModule)
    )]
public class FlowerShopApplicationTestModule : AbpModule
{

}
