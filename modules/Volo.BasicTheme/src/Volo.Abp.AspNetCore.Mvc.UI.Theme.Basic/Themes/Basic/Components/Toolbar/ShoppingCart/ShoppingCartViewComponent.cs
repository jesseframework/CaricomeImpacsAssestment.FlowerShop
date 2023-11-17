using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic.Themes.Basic.Components.Toolbar.UserMenu;

public class ShoppingCartViewComponent : AbpViewComponent
{
    protected IMenuManager MenuManager { get; }

    public ShoppingCartViewComponent(IMenuManager menuManager)
    {
        MenuManager = menuManager;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var menu = await MenuManager.GetAsync(StandardMenus.User);
        return View("~/Themes/Basic/Components/Toolbar/ShoppingCart/Default.cshtml", menu);
    }
}
