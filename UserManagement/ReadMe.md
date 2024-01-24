## Authp multitenant UserManagement with Blazor 8

This project aims to establish a unified user identity for multitenant environments across multiple projects. The implementation relies on the AuthPermissions.AspNetCore library developed by John P Smith, available at https://github.com/JonPSmith/AuthPermissions.AspNetCore.

* One of the challenges I encountered while implementing the AuthP library is understanding and adapting MVC code to Blazor.
* Another significant hurdle is the absence of examples for creating multitenancy exclusively for the Identity User.
* English is not my native language, so please forgive any grammar errors or sentences that may not be entirely correct.

## Create Custom Field DateCreated in AspNetUser table

To enhance the functionality of the project, I intend to include a "DateCreated" field in the AspNetUser table. This field is defined in the IdentityUser100 class project within the configuration folder, specifically in the ApplicationUserConfig.cs file.

The default value for "DateCreated" is set using the following line of code:

`builder.Property(b => b.DateCreated).HasDefaultValueSql("GETDATE()");`

```
 public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            builder.Property(b => b.Id).HasColumnType("varchar(256)");
            builder.Property(b => b.UserName).HasColumnType("varchar(256)");
            builder.Property(b => b.NormalizedUserName).HasColumnType("varchar(256)");
            builder.Property(b => b.Email).HasColumnType("varchar(256)");
            builder.Property(b => b.NormalizedEmail).HasColumnType("varchar(256)");
            builder.Property(b => b.PasswordHash).HasColumnType("varchar(256)");
            builder.Property(b => b.SecurityStamp).HasColumnType("varchar(256)");
            builder.Property(b => b.ConcurrencyStamp).HasColumnType("varchar(256)");
            builder.Property(b => b.PhoneNumber).HasColumnType("varchar(16)");
            builder.Property(b => b.Name).HasColumnType("varchar(100)");

            builder.Property(b => b.DateCreated).HasDefaultValueSql("GETDATE()");

        }
```

## Create Blazor Server Side Rendering.

For the user interface, I'm utilizing the FluentUI Blazor Component, accessible at https://www.fluentui-blazor.net/. The project is set up as a Blazor Server-Side Rendering with Individual Accounts.
  
    * Choose Authentication Type : Invidual Accounts.
    * Interactive Render Mode = None
    
  ![Create Project](/wwwroot/image/BlazorSSR.png)

To ensure a backup, create a "Backup" folder and a "Step1" folder. Copy the project into the "Step1" folder as a precaution in case of any issues.

## Implenting IdentityUser100

* Delete the "Data" folder. We will utilize the class project IdentityUser100.
* Reference the IdentityUser class project in the UserManagement project.
* Change `ApplicationUser` to `IdentityUser` and `ApplicationDbContext` to `IdentityUser100DbContext`.
* Create a backup in the Step2 folder.

## Create AuthpServices Class Project

* Add the Authp NuGet Package, version 6.1.0.
* Create a "PermissionCode" folder and add `AppAuthSetupData.cs` and `UserPermission.cs`.

## Implementing Multitenancy in the UserManagement Project

* Reference the AuthpServices class project.
* Examine program.cs and appsettings.json to identify added configurations.
* In the "Pages" folder, create the "HomeController" folder and "TenantAdminController."
* Add "TenantCreate.razor" in the "HomeController" folder. This user interface, TenantCreate.razor, can only be used by user who has `TenantCreate` permission.
  So there are only two users who can create tenant which are `Super@g1.com`, and `AppAdmin@g1.com`.
* Add "InviteUser.razor" and "AcceptInvitation.razor" in the "TenantAdminController."  
  These user interfaces are used when the tenant wants users to sign up only by invitation.
* When first running the application, two users, `Super@g1.com`, and `AppAdmin@g1.com`, will be created. 
  Please change "EmailConfirmed" from False to True so that you can log in using these two users to create tenants. The password is the same as the username.

