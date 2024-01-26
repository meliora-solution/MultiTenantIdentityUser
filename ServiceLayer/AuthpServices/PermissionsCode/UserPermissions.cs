using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthpServices.PermissionsCode
{
    public enum UserPermissions : ushort
    {
        NotSet = 0, //error condition

        [Display(GroupName = "User", Name = "Access page",Description = "Can Access Page")]
        AccessPageLevelOne = 10,



        [Display(GroupName = "Employees", Description = "Can invite new users to join the tenant")]
        InviteUsers = 30_000,

        //Admin section

        //40_000 - User admin
        [Display(GroupName = "UserAdmin", Name = "Read users", Description = "Can list User")]
        UserRead = 40_000,
        [Display(GroupName = "UserAdmin", Name = "Sync users", Description = "Syncs authorization provider with AuthUsers")]
        UserSync = 40_001,
        [Display(GroupName = "UserAdmin", Name = "Alter users", Description = "Can access the user update")]
        UserChange = 40_002,
        [Display(GroupName = "UserAdmin", Name = "Alter user's roles", Description = "Can add/remove roles from a user")]
        UserRolesChange = 40_003,
        [Display(GroupName = "UserAdmin", Name = "Move a user to another tenant", Description = "Can control what tenant they are in")]
        UserChangeTenant = 40_004,
        [Display(GroupName = "UserAdmin", Name = "Remove user", Description = "Can delete the user")]
        UserRemove = 40_005,

        //41_000 - Roles admin
        [Display(GroupName = "RolesAdmin", Name = "Read Roles", Description = "Can list Role")]
        RoleRead = 41_000,
        //This is an example of grouping multiple actions under one permission
        [Display(GroupName = "RolesAdmin", Name = "Change Role", Description = "Can create, update or delete a Role", AutoGenerateFilter = true)]
        RoleChange = 41_001,

        //41_100 - Permissions 
        [Display(GroupName = "RolesAdmin", Name = "See permissions", Description = "Can display the list of permissions", AutoGenerateFilter = true)]
        PermissionRead = 41_100,
        [Display(GroupName = "RolesAdmin", Name = "See all permissions", Description = "list will included filtered Permission ", AutoGenerateFilter = true)]
        IncludeFilteredPermissions = 41_101,

        //42_000 - tenant admin
        [Display(GroupName = "TenantAdmin", Name = "Read Tenants", Description = "Can list Tenants")]
        TenantList = 42_000,
        [Display(GroupName = "TenantAdmin", Name = "Create new Tenant", Description = "Can create new Tenants", AutoGenerateFilter = true)]
        TenantCreate = 42_001,
        [Display(GroupName = "TenantAdmin", Name = "Alter Tenants info", Description = "Can update Tenant's name", AutoGenerateFilter = true)]
        TenantUpdate = 42_002,
        [Display(GroupName = "TenantAdmin", Name = "Move tenant to another parent", Description = "Can move tenant to different parent (WARNING)", AutoGenerateFilter = true)]
        TenantMove = 42_003,
        [Display(GroupName = "TenantAdmin", Name = "Delete tenant", Description = "Can delete tenant (WARNING)", AutoGenerateFilter = true)]
        TenantDelete = 42_004,
        [Display(GroupName = "TenantAdmin", Name = "Access other tenant data", Description = "Sets DataKey of user to another tenant", AutoGenerateFilter = true)]
        TenantAccessData = 42_005,

        //Setting the AutoGenerateFilter to true in the display allows we can exclude this permissions
        //to admin users who aren't allowed alter this permissions
        //Useful for multi-tenant applications where you can set up company-level admin users where you can hide some higher-level permissions
        [Display(GroupName = "SuperAdmin", Name = "AccessAll",
            Description = "This allows the user to access every feature", AutoGenerateFilter = true)]
        AccessAll = ushort.MaxValue,

    }
}
