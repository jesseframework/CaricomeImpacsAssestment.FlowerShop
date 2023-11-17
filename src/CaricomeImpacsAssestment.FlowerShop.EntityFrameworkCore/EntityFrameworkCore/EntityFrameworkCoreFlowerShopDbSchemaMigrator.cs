using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CaricomeImpacsAssestment.FlowerShop.Data;
using Volo.Abp.DependencyInjection;

namespace CaricomeImpacsAssestment.FlowerShop.EntityFrameworkCore;

public class EntityFrameworkCoreFlowerShopDbSchemaMigrator
    : IFlowerShopDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreFlowerShopDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the FlowerShopDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<FlowerShopDbContext>()
            .Database
            .MigrateAsync();
    }
}
