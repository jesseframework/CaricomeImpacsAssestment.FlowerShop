using CaricomeImpacsAssestment.FlowerShop.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class FlowerShopPageModel : AbpPageModel
{
    protected FlowerShopPageModel()
    {
        LocalizationResourceType = typeof(FlowerShopResource);
    }
}
