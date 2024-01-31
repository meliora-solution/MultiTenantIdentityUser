using AuthPermissions;
using AuthPermissions.AspNetCore;
using AuthPermissions.AspNetCore.Services;
using AuthPermissions.AspNetCore.StartupServices;
using AuthPermissions.BaseCode;
using AuthPermissions.BaseCode.SetupCode;
using AuthPermissions.SupportCode.AddUsersServices;
using AuthPermissions.SupportCode.AddUsersServices.Authentication;
using AuthpServices.PermissionsCode;
using EasyStockDb.Context;
using EasyStockServices.Extensions;
using IdentityUser100.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RunMethodsSequentially;

using Swashbuckle.AspNetCore.Filters;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

builder.Services.AddDbContext<IdentityUser100DbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Add services to the container.

builder.Services.AddDbContext<EasyStockDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EasyStockDbConnection")));


builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<IdentityUser100DbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddEasyStockServices();

//builder.Services.AddScoped<EasyStockContactServices>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGroup("api/auth-default").MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
