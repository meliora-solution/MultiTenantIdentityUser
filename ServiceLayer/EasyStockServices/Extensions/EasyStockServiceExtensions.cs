using Microsoft.Extensions.DependencyInjection;



namespace EasyStockServices.Extensions
{
    public static class EasyStockServiceExtensions
    {
        public static void AddEasyStockServices(this IServiceCollection services)
        {

            /*EasyStock Services*/
            services.AddScoped<EasyStockContactServices>();
        }
    }
}
