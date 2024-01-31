using AuthPermissions.AspNetCore.GetDataKeyCode;
using CodeFirstDbDesign.Components;
using EasyStockDb.Context;
using IdentityUser100.Context;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<IdentityUser100DbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDbContext<EasyStockDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EasyStockDbConnection"));
});


builder.Services.AddTransient<IGetDataKeyFromUser, GetDataKeyFromUserNormal>();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
