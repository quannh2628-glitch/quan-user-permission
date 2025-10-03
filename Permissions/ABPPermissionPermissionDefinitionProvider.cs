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

        // Author permissions
        var authorPermission = myGroup.AddPermission(ABPPermissionPermissions.Authors.Default, L("Permission:Authors"));
        authorPermission.AddChild(ABPPermissionPermissions.Authors.Create, L("Permission:Authors.Create"));
        authorPermission.AddChild(ABPPermissionPermissions.Authors.Edit, L("Permission:Authors.Edit"));
        authorPermission.AddChild(ABPPermissionPermissions.Authors.Delete, L("Permission:Authors.Delete"));

        // Customer permissions
        var customerPermission = myGroup.AddPermission(ABPPermissionPermissions.Customers.Default, L("Permission:Customers"));
        customerPermission.AddChild(ABPPermissionPermissions.Customers.Create, L("Permission:Customers.Create"));
        customerPermission.AddChild(ABPPermissionPermissions.Customers.Edit, L("Permission:Customers.Edit"));
        customerPermission.AddChild(ABPPermissionPermissions.Customers.Delete, L("Permission:Customers.Delete"));
        customerPermission.AddChild(ABPPermissionPermissions.Customers.InitData, L("Permission:Customers.InitData"));

        // UserManagement permissions
        var userMgmt = myGroup.AddPermission(ABPPermissionPermissions.UserManagement.Default, L("Permission:UserManagement"));
        userMgmt.AddChild(ABPPermissionPermissions.UserManagement.CreateBatch, L("Permission:UserManagement.CreateBatch"));
        userMgmt.AddChild(ABPPermissionPermissions.UserManagement.FillDob, L("Permission:UserManagement.FillDob"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ABPPermissionResource>(name);
    }
}
