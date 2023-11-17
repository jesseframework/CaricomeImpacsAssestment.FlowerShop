using CaricomeImpacsAssestment.FlowerShop.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CaricomeImpacsAssestment.FlowerShop.Permissions;

public class FlowerShopPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(FlowerShopPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(FlowerShopPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FlowerShopResource>(name);
    }
}
