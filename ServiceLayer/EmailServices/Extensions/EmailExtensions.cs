using MailServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MailServices.Extensions
{
    public static class EmailExtensions
    {
        public static void AddEmailServices(this IServiceCollection services)
        {
            /* Email Services */
            services.AddScoped<EmailServices>();
            services.AddScoped<SendEmailServices>();
            services.AddScoped<DecodingServices>();
            services.AddScoped<EncodingServices>();


        }
    }
}
