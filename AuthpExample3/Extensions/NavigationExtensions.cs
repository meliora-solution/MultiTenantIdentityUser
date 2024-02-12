using AuthpExample3.Services;


namespace AuthpExample3.Extensions
{
    public static class NavigationExtensions
    {
        public static void AddNavigationServices(this IServiceCollection services)
        {
            services.AddScoped<NavigationService>();

        }
    }
}
