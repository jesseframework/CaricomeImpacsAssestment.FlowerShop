using CaricomeImpacsAssestment.FlowerShop.Localization;
using CaricomeImpacsAssestment.FlowerShop.MultiTenancy;
using CaricomeImpacsAssestment.FlowerShop.Permissions;
using Microsoft.AspNetCore.Authorization;
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

        
        var l = context.GetLocalizer<FlowerShopResource>();
        if (await context.IsGrantedAsync(IdentityMenuNames.Users))
        {
            // Create the management group
            var managementGroup = new ApplicationMenuItem(
                "Management",
                l["Management"],
                icon: "fas fa-receipt"
            );

            context.Menu.AddItem(managementGroup);

            // Add sub-items to the management group
            managementGroup.AddItem(
                new ApplicationMenuItem(
                    "Management.Customers",
                    l["Customer"],
                    icon: "fas fa-user",
                    url: "/Admin/Customer"
                )
            );

            managementGroup.AddItem(
                new ApplicationMenuItem(
                    "Management.Dashboard",
                    l["Dashboard"],
                    icon: "fas fa-chart-bar",
                    url: "/Admin/Dashboard"
                )
            );

            managementGroup.AddItem(
                new ApplicationMenuItem(
                    "Management.Orders",
                    l["Order Management"],
                    icon: "fas fa-shopping-cart",
                    url: "/Admin/Order"
                )
            );

            managementGroup.AddItem(
                new ApplicationMenuItem(
                    "Management.Products",
                    l["Product Management"],
                    icon: "fas fa-layer-group",
                    url: "/Admin/Product/Items"
                )
            );
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
            //    context.Menu.AddItem(
            //   new ApplicationMenuItem(
            //       "Management",
            //       l["Management"],
            //       icon: "fas fa-receipt"
            //   ).AddItem(
            //       new ApplicationMenuItem(
            //           "AddCustomer",
            //           l["Customer"],
            //           icon: "fas fa-user",
            //           url: "/Admin/Customer"
            //       )
            //   ).AddItem(
            //       new ApplicationMenuItem(
            //           "AddItems",
            //           l["Dashboard"],
            //           icon: "fas fa-chart-bar",
            //           url: "/Admin/Dashboard"
            //       )
            //).AddItem(
            //       new ApplicationMenuItem(
            //           "AddItems",
            //           l["Order Management"],
            //           icon: "fas fa-shopping-cart",
            //           url: "/Admin/Order"
            //       )
            //)
            //.AddItem(
            //       new ApplicationMenuItem(
            //           "AddItems",
            //           l["Product Management"],
            //           icon: "fas fa-layer-group",
            //           url: "/Admin/Product/Items"
            //       )
            //));
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
