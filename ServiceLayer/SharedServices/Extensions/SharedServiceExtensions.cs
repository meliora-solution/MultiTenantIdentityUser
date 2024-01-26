using Microsoft.Extensions.DependencyInjection;
using SharedServices.Services.Email;

namespace AuthpServices.Extensions
{
    public static class SharedServiceExtensions
    {
        public static void AddSharedServices(this IServiceCollection services)
        {
          
            services.AddScoped<EmailServices>();
            services.AddScoped<SendEmailServices>();
            services.AddScoped<DecodingServices>();
            services.AddScoped<EncodingServices>();
        }
    }
}
