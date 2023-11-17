using CaricomeImpacsAssestment.FlowerShop.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace CaricomeImpacsAssestment.FlowerShop;

[DependsOn(
    typeof(AbpAuditLoggingDomainSharedModule),
    typeof(AbpBackgroundJobsDomainSharedModule),
    typeof(AbpFeatureManagementDomainSharedModule),
    typeof(AbpIdentityDomainSharedModule),
    typeof(AbpOpenIddictDomainSharedModule),
    typeof(AbpPermissionManagementDomainSharedModule),
    typeof(AbpSettingManagementDomainSharedModule),
    typeof(AbpTenantManagementDomainSharedModule)    
    )]
public class FlowerShopDomainSharedModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        FlowerShopGlobalFeatureConfigurator.Configure();
        FlowerShopModuleExtensionConfigurator.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<FlowerShopDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<FlowerShopResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/FlowerShop");

            options.DefaultResourceType = typeof(FlowerShopResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("FlowerShop", typeof(FlowerShopResource));
        });
    }
}
