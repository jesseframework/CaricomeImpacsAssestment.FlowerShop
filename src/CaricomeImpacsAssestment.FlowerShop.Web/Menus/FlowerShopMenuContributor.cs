using CaricomeImpacsAssestment.FlowerShop.Localization;
using CaricomeImpacsAssestment.FlowerShop.MultiTenancy;
using System.Threading.Tasks;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace CaricomeImpacsAssestment.FlowerShop.Web.Menus;

public class FlowerShopMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<FlowerShopResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                FlowerShopMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );
        context.Menu.Items.Insert(
        1,
        new ApplicationMenuItem(
            "Shop",
            l["Shop"],
            "/shop",
            icon: "fas fa-store",
            order: 1
        )
    );

       


        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);

                        
            //ToDo Remove and create admin olices
            context.Menu.AddItem(
               new ApplicationMenuItem(
                   "Management",
                   l["Management"],
                   icon: "fa fa-system"
               ).AddItem(
                   new ApplicationMenuItem(
                       "AddCustomer",
                       l["Customer"],
                       icon: "fas fa-user",
                       url: "/Admin/Customer"
                   )
               ).AddItem(
                   new ApplicationMenuItem(
                       "AddItems",
                       l["Items"],
                       icon: "fas fa-stock",
                       url: "/Items"
                   )
            ));
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }

   
}
