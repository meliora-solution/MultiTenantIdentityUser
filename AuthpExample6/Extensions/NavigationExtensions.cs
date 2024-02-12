using AuthpExample6.Services;


namespace AuthpExample6.Extensions
{
    public static class NavigationExtensions
    {
        public static void AddNavigationServices(this IServiceCollection services)
        {
            services.AddScoped<NavigationService>();

        }
    }
}
