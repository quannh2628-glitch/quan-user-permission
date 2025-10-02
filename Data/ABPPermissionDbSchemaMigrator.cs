using Volo.Abp.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ABPPermission.Data;

public class ABPPermissionDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public ABPPermissionDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        
        /* We intentionally resolving the ABPPermissionDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ABPPermissionDbContext>()
            .Database
            .MigrateAsync();

    }
}
