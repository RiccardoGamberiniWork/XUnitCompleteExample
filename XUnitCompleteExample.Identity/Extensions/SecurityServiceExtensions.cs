namespace XUnitCompleteExample.Identity.Extensions
{
    public static class SecurityServiceExtensions
    {
        public static IServiceCollection AddSecurityServices(this IServiceCollection services)
        {           
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            return services;
        }
    }
}