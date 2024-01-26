using AuthpServices.SupportCode.AddUserServices;
using Microsoft.Extensions.DependencyInjection;

namespace AuthpServices.Extensions
{
    public static class AuthpServiceExtensions
    {
        public static void AddAuthpServices(this IServiceCollection services)
        {
            services.AddScoped<CreateTenant>();
            services.AddScoped<InviteNewUser>();
        }
    }
}
