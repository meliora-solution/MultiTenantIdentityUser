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

## Create Tenant, InviteUser, Accept Invitation

* 01/25/2024
* On AuthPServices/SupportCode/AddUserServices, CreateTenant and InviteUser classes.  I just remove automatic signin from the original code.  The reason is that when I create an invitation and accept invitation and the user is signin,  I am forced to logout.  I can not create another invitation.  So for this testing environtment, I dont want user who accept invitation can signin automatically.
* Create a new class project, SharedServices.  For now, I just add for email services so I can SendEmail and Admin Tenant or user can confirm the email.
* To use email services you need to change. Change it to your real email sender.
  
  **Configure SMTP Settings in appsettings.json**
    
    ```
    
    "SMTP": {
        "Host": "mail.yourMailServer.com",
        "Port": "465",
        "User": "User@yourMailServer.com",
        "Password": "AutpMailServerPassword123"
      },
    ``` 
  **Update EmailMessageServices.cs in SharedServices/Services/Email**
  
    `message.From.Add(new MailboxAddress("Sender", "sender@yourMailServer.com"));` to your real email sender.
  
* **User Interfaces**.  
  I created two types of user interface, manually for testing and send by Email.
  

    * **TenantCreate.** Creating a tenant and confirmEmail using the code provided by samples from Microsoft.
    * **TenantCreateSendEmail.**  Creating a tenant, sending an email and confirming the Email.
    * **InviteUser.**  Inviting a user with only one input Email. The invitation link will direct users to the AcceptInvitation user interface.
    * **InviteUserByEmail.**  This interface has three inputs: Email, selecting InviteExpirationTime, and multiselect RoleNames. It creates an invitation link and sends it to an email.
    * **AcceptInvitation.**  his interface has two input fields: email and password. If the email matches the invitation email, the user will be created. To confirm, the user still has to click on ConfirmEmail.
    * **AcceptInvitationByEmail.**   If the user accepts the invitation from the email, the user will be redirected to this interface. Confirm Email will be done automatically.
    
    So for my usecase, tenantcreate, InviteUser and AcceptInvitation are user interfaces for testing.

* **Authorization.**  
  
  One nice feature of this authp library is that I can easily hide the navigation links if the user does not have permission.
  Initially, when the user does not have permission, they will be redirected to the 'AccessDenied' page because the user does not have permission.
  This can be inconvenient and frustrating for the user.

  ```
     @if (user.HasPermission(UserPermissions.TenantList))
    {
      <FluentNavLink Href="tenantlist" Icon="@(new Icons.Regular.Size20.PeopleCommunity())" IconColor="Color.Accent">Tenant List</FluentNavLink>
    }
    @if (user.HasPermission(UserPermissions.TenantCreate))
    {
      <FluentNavLink Href="tenantCreate" Icon="@(new Icons.Regular.Size20.PeopleCommunityAdd())" IconColor="Color.Accent">Tenant Create</FluentNavLink>
      <FluentNavLink Href="tenantCreateSendEmail" Icon="@(new Icons.Regular.Size20.PeopleCommunityAdd())" IconColor="Color.Accent">Tenant Create By Email</FluentNavLink>
    }
    @if (user.HasPermission(UserPermissions.InviteUsers))
    {
      <FluentNavLink Href="inviteUser" Icon="@(new Icons.Regular.Size20.PeopleCommunityAdd())" IconColor="Color.Accent">Invite User</FluentNavLink>
      <FluentNavLink Href="inviteUserByEmail" Icon="@(new Icons.Regular.Size20.PeopleCommunityAdd())" IconColor="Color.Accent">Invite User By Email</FluentNavLink>
    }
  ```

  ## 01/27/2023

* Create a sample database with only one table contact (Id,Name,Address)
* Create web api project.

