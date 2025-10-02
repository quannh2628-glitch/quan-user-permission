using ABPPermission.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace ABPPermission.Permissions;

public class ABPPermissionPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ABPPermissionPermissions.GroupName);


        myGroup.AddPermission(ABPPermissionPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);

        var booksPermission = myGroup.AddPermission(ABPPermissionPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(ABPPermissionPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(ABPPermissionPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(ABPPermissionPermissions.Books.Delete, L("Permission:Books.Delete"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(ABPPermissionPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ABPPermissionResource>(name);
    }
}
