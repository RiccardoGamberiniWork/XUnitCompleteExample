namespace XUnitCompleteExample.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<HttpClientConfig>(config.GetSection("HttpClientConfiguration"));
        return services;
    }
}