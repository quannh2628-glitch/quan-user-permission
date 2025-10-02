using Volo.Abp.Threading;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Identity;

namespace ABPPermission.Data;

public static class ABPPermissionEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        ABPPermissionGlobalFeatureConfigurator.Configure();
        ABPPermissionModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
            /* You can configure extra properties for the
             * entities defined in the modules used by your application.
             *
             * This class can be used to map these extra properties to table fields in the database.
             *
             * USE THIS CLASS ONLY TO CONFIGURE EF CORE RELATED MAPPING.
             * USE ABPPermissionModuleExtensionConfigurator CLASS (in the Domain.Shared project)
             * FOR A HIGH LEVEL API TO DEFINE EXTRA PROPERTIES TO ENTITIES OF THE USED MODULES
             *
             * Example: Map a property to a table field:

                 ObjectExtensionManager.Instance
                     .MapEfCoreProperty<IdentityUser, string>(
                         "MyProperty",
                         (entityBuilder, propertyBuilder) =>
                         {
                             propertyBuilder.HasMaxLength(128);
                         }
                     );

             * See the documentation for more:
             * https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Extending-Entities
             */

            // Map DateOfBirth (nullable) cho bảng AbpUsers
            ObjectExtensionManager.Instance
                .MapEfCoreProperty<IdentityUser, System.DateTime?>(
                    "DateOfBirth",
                    (entityBuilder, propertyBuilder) =>
                    {
                        // Không bắt buộc, không cần max length; có thể set column type nếu muốn
                        propertyBuilder.IsRequired(false);
                    }
                );
        });
    }
}
