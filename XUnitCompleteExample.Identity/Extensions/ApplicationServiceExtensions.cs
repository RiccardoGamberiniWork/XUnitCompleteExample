namespace XUnitCompleteExample.Identity.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();
        services.AddDbContext<XUnitCompleteExampleIdentityDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DBConnection"));
        });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });
        services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");// Impostiamo il formato della versione.
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfig>();
        services.AddSwaggerGen();
        services.AddOptions();
        services.Configure<JwtConfig>(config.GetSection("JWTAuth"));
        services.Configure<LdapConfig>(config.GetSection("Ldap"));
        services.Configure<AppSettingsConfig>(config.GetSection("AppSettings"));
        return services;
    }
}