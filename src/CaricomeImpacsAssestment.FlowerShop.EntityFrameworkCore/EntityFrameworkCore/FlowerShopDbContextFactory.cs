using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CaricomeImpacsAssestment.FlowerShop.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class FlowerShopDbContextFactory : IDesignTimeDbContextFactory<FlowerShopDbContext>
{
    public FlowerShopDbContext CreateDbContext(string[] args)
    {
        FlowerShopEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<FlowerShopDbContext>()
            .UseMySql(configuration.GetConnectionString("Default"), MySqlServerVersion.LatestSupportedServerVersion);

        return new FlowerShopDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CaricomeImpacsAssestment.FlowerShop.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
