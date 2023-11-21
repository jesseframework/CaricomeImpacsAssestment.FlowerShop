using CaricomeImpacsAssestment.FlowerShop.Customer;
using CaricomeImpacsAssestment.FlowerShop.Order;
using CaricomeImpacsAssestment.FlowerShop.Payment;
using CaricomeImpacsAssestment.FlowerShop.Product;
using CaricomeImpacsAssestment.FlowerShop.Settings;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace CaricomeImpacsAssestment.FlowerShop.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class FlowerShopDbContext :
    AbpDbContext<FlowerShopDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    //FlowerShop
    public DbSet<Address> Addresses { get; set; }
    public DbSet<AddressType> AddressTypes { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<CustomerAccount> CustomerAccounts { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetailTemp> OrderDetailTemp { get; set; }
    public DbSet<OrderPayment> OrderPayments { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemPrice> ItemPrices { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<Tax> Taxes { get; set; }
    public DbSet<CookieTracker> CookieTrackers { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<SerialNumber> SerialNumbers { get; set; }




    #endregion

    public FlowerShopDbContext(DbContextOptions<FlowerShopDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(FlowerShopConsts.DbTablePrefix + "YourEntities", FlowerShopConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<Address>(b =>
        {
            b.ToTable(FlowerShopConsts.DbTablePrefix + "Addresses",
                FlowerShopConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne<Country>().WithMany().HasForeignKey(x => x.CountryId);
            b.HasOne<AddressType>().WithMany().HasForeignKey(x => x.AddressTypeId);


        });

        builder.Entity<AddressType>(b =>
        {
            b.ToTable(FlowerShopConsts.DbTablePrefix + "AddressTypes",
                FlowerShopConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.Type).IsUnique();


        });

        builder.Entity<Contact>(b =>
        {
            b.ToTable(FlowerShopConsts.DbTablePrefix + "Contacts",
                FlowerShopConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => new { x.FirstName, x.LastName, x.Email }).IsUnique();
        });

        builder.Entity<Country>(b =>
        {
            b.ToTable(FlowerShopConsts.DbTablePrefix + "Countries",
                FlowerShopConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.Code).IsUnique();


        });
        builder.Entity<Currency>(b =>
        {
            b.ToTable(FlowerShopConsts.DbTablePrefix + "Currencies",
                FlowerShopConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.CurrencyCode).IsUnique();


        });

        builder.Entity<CustomerAccount>(b =>
        {
            b.ToTable(FlowerShopConsts.DbTablePrefix + "CustomerAccounts",
                FlowerShopConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne<Country>().WithMany().HasForeignKey(x => x.CountryId);
            b.HasOne<Contact>().WithMany().HasForeignKey(x => x.ContactId);
            b.HasOne<Address>().WithMany().HasForeignKey(x => x.BillingAddressId);
            b.HasOne<Address>().WithMany().HasForeignKey(x => x.ShippingAddressId);
            b.HasOne<Currency>().WithMany().HasForeignKey(x => x.CurrencyId);

        });

        builder.Entity<Country>(b =>
        {
            b.ToTable(FlowerShopConsts.DbTablePrefix + "Countries",
                FlowerShopConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.Code).IsUnique();

        });

        builder.Entity<OrderHeader>(b =>
        {
            b.ToTable(FlowerShopConsts.DbTablePrefix + "OrderHeaders",
                FlowerShopConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne<Address>().WithMany().HasForeignKey(x => x.BillToAddressId);
            b.HasOne<Address>().WithMany().HasForeignKey(x => x.ShipToAddressId);
            b.HasOne<CustomerAccount>().WithMany().HasForeignKey(x => x.CustomerAccountId);
            //b.HasIndex(x => x.OrderNo).IsUnique(); remove for not not enougt time to implement logice for order number
        });
        builder.Entity<OrderDetail>(b =>
        {
            b.ToTable(FlowerShopConsts.DbTablePrefix + "OrderDetails",
                FlowerShopConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId);
            b.HasOne<ProductGroup>().WithMany().HasForeignKey(x => x.ProductGroupId);
            b.HasOne<Item>().WithMany().HasForeignKey(x => x.ItemId);
            b.HasOne<OrderHeader>().WithMany().HasForeignKey(x => x.OrderHeaderId);

        });
        builder.Entity<OrderDetailTemp>(b =>
        {
            b.ToTable(FlowerShopConsts.DbTablePrefix + "OrderDetailTemp",
                FlowerShopConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId);
            b.HasOne<ProductGroup>().WithMany().HasForeignKey(x => x.ProductGroupId);
            b.HasOne<Item>().WithMany().HasForeignKey(x => x.ItemId);


        });
        builder.Entity<OrderPayment>(b =>
        {
            b.ToTable(FlowerShopConsts.DbTablePrefix + "OrderPayments",
                FlowerShopConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne<CustomerAccount>().WithMany().HasForeignKey(x => x.CustomerAccountId);
            b.HasOne<OrderHeader>().WithMany().HasForeignKey(x => x.OrderHeaderId);
        });

        builder.Entity<Category>(b =>
        {
            b.ToTable(FlowerShopConsts.DbTablePrefix + "Categories",
                FlowerShopConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.Name).IsUnique();

        });
        builder.Entity<Item>(b =>
        {
            b.ToTable(FlowerShopConsts.DbTablePrefix + "Items",
                FlowerShopConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId);
            b.HasOne<ProductGroup>().WithMany().HasForeignKey(x => x.ProductGroupId);
            b.HasIndex(x => x.ItemNo).IsUnique();
        });
        builder.Entity<ProductGroup>(b =>
        {
            b.ToTable(FlowerShopConsts.DbTablePrefix + "ProductGroups",
                FlowerShopConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.Name).IsUnique();

        });
        builder.Entity<Tax>(b =>
        {
            b.ToTable(FlowerShopConsts.DbTablePrefix + "Taxes",
                FlowerShopConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.TaxCode).IsUnique();

        });
    }
}
