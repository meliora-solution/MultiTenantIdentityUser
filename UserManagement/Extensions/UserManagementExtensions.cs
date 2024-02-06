using UserManagement.Services;

namespace UserManagement.Extensions
{
    public static class UserManagementExtensions
    {
        public static void AddUserManagementServices(this IServiceCollection services)
        {
            services.AddScoped<NavigationService>();

        }
    }
}
