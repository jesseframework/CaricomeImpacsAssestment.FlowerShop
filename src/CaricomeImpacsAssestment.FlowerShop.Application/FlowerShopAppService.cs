using System;
using System.Collections.Generic;
using System.Text;
using CaricomeImpacsAssestment.FlowerShop.Localization;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop;

/* Inherit your application services from this class.
 */
public abstract class FlowerShopAppService : ApplicationService
{
    protected FlowerShopAppService()
    {
        LocalizationResource = typeof(FlowerShopResource);
    }
}
