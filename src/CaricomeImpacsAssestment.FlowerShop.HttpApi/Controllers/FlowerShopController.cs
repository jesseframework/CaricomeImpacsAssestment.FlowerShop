using CaricomeImpacsAssestment.FlowerShop.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CaricomeImpacsAssestment.FlowerShop.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class FlowerShopController : AbpControllerBase
{
    protected FlowerShopController()
    {
        LocalizationResource = typeof(FlowerShopResource);
    }
}
