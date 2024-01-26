using AuthPermissions.AspNetCore.Services;
using AuthPermissions;
using AuthpServices.PermissionsCode;
using IdentityUser100.Context;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagement.Components;
using UserManagement.Components.Account;
using AuthPermissions.AspNetCore;
using RunMethodsSequentially;
using AuthPermissions.AspNetCore.StartupServices;
using Microsoft.FluentUI.AspNetCore.Components;
using AuthPermissions.SupportCode.AddUsersServices.Authentication;
using AuthPermissions.SupportCode.AddUsersServices;
using AuthPermissions.BaseCode.SetupCode;
using AuthPermissions.BaseCode;
using AuthpServices.Extensions;
using SharedServices.Models.Email;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddFluentUIComponents();


builder.Services.AddAuthpServices();
// Register SmtpSettings
#region "From SharedServices"

builder.Services.AddSharedServices();

var SmtpSettingsSection = builder.Configuration.GetSection("SMTP");
builder.Services.Configure<SmtpSetting>(SmtpSettingsSection);

#endregion


// Add services to the container.
builder.Services.AddRazorComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();


builder.Services.AddDbContext<IdentityUser100DbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityUser100DbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<IdentityUser>, IdentityNoOpEmailSender>();


builder.Services.RegisterAuthPermissions<UserPermissions>(options =>
{
   
    options.UseLocksToUpdateGlobalResources = false;
    options.TenantType = TenantTypes.SingleLevel;
    options.LinkToTenantType = LinkToTenantTypes.OnlyAppUsers;
    options.EncryptionKey = builder.Configuration[nameof(AuthPermissionsOptions.EncryptionKey)];
    options.PathToFolderToLock = builder.Environment.WebRootPath;

})
   

    .UsingEfCoreSqlServer(connectionString) //NOTE: This uses the same database as the individual accounts DB
    .IndividualAccountsAuthentication()
    .AddSuperUserToIndividualAccounts()
  
    .RegisterFindUserInfoService<IndividualAccountUserLookup>()
    .AddRolesPermissionsIfEmpty(AppAuthSetupData.RolesDefinition)
    .AddAuthUsersIfEmpty(AppAuthSetupData.UsersRolesDefinition)

    .RegisterFindUserInfoService<IndividualAccountUserLookup>()
    .RegisterAuthenticationProviderReader<SyncIndividualAccountUsers>()

    .SetupAspNetCoreAndDatabase(options =>
    {
        //Migrate individual account database
        options.RegisterServiceToRunInJob<StartupServiceMigrateAnyDbContext<IdentityUser100DbContext>>();
        //Add demo users to the database
        options.RegisterServiceToRunInJob<StartupServicesIndividualAccountsAddDemoUsers>();
    });

//manually add services from the AuthPermissions.SupportCode project
//Add the SupportCode services
builder.Services.AddTransient<IAddNewUserManager, IndividualUserAddUserManager<IdentityUser>>();
builder.Services.AddTransient<ISignInAndCreateTenant, SignInAndCreateTenant>();
builder.Services.AddTransient<IInviteNewUserService, InviteNewUserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

// must be put below app.UseAuthentication();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();

