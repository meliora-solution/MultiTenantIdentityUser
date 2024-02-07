using AuthPermissions.BaseCode.DataLayer.Classes.SupportTypes;
using AuthPermissions.BaseCode.SetupCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthpServices.PermissionsCode
{
    public static class AppAuthSetupData
    {
        public static readonly List<BulkLoadRolesDto> RolesDefinition = new List<BulkLoadRolesDto>()
        {
            new("SuperAdmin", "Super admin - only use for setup", "AccessAll"),
            new("App Admin", "Overall app Admin",
                "UserRead, UserSync, UserChange, UserRemove, " +
                "UserRolesChange, RoleRead, RoleChange, PermissionRead, IncludeFilteredPermissions, " +
                "TenantList, TenantCreate, TenantUpdate, UserChangeTenant, TenantAccessData"),
           new("App Support", "overall support - limited admin items",
                "UserRead, UserRolesChange, RoleRead, TenantList, TenantAccessData"),
            //tenant roles
            new("Tenant Admin", "Tenant-level admin",
                "UserRead, UserRolesChange, RoleRead, InviteUsers", RoleTypes.TenantAdminAdd),
            // User Guest
            new("User Guest", "User-level Guest", "AccessPageLevelOne"),

        };

        public static readonly List<BulkLoadUserWithRolesTenant> UsersRolesDefinition = new List<BulkLoadUserWithRolesTenant>
        {
            new ("Super@g1.com", null, "SuperAdmin"),
            new ("AppAdmin@g1.com", null, "App Admin"),
            new("AppSupport@g1.com", null, "App Support"),

        };
    }
}

