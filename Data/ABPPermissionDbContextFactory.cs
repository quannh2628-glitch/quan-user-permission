using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ABPPermission.Data;

public class ABPPermissionDbContextFactory : IDesignTimeDbContextFactory<ABPPermissionDbContext>
{
    public ABPPermissionDbContext CreateDbContext(string[] args)
    {
        ABPPermissionGlobalFeatureConfigurator.Configure();
        ABPPermissionModuleExtensionConfigurator.Configure();

        ABPPermissionEfCoreEntityExtensionMappings.Configure();
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<ABPPermissionDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new ABPPermissionDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}