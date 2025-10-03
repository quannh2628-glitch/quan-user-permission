namespace ABPPermission.Permissions;

public static class ABPPermissionPermissions
{
    public const string GroupName = "ABPPermission";

    public static class Dashboard
    {
        public const string DashboardGroup = GroupName + ".Dashboard";
        public const string Host = DashboardGroup + ".Host";
    }

    public static class Books
    {
        public const string Default = GroupName + ".Books";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class UserManagement
    {
        public const string Default = GroupName + ".UserManagement";
        public const string CreateBatch = Default + ".CreateBatch"; // init 100 users
        public const string FillDob = Default + ".FillDob";       // fill DateOfBirth
    }

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
