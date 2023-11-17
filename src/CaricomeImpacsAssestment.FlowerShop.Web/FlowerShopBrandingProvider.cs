using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace CaricomeImpacsAssestment.FlowerShop.Web;

[Dependency(ReplaceServices = true)]
public class FlowerShopBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "FlowerShop";
}
