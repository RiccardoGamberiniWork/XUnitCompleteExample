namespace XUnitCompleteExample.Identity.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            ////services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            ////    .AddJwtBearer(options=> 
            ////    {
            ////        options.TokenValidationParameters = new TokenValidationParameters
            ////        {
            ////            ValidateIssuerSigningKey = true,
            ////            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
            ////            ValidateIssuer = false,
            ////            ValidateAudience = false
            ////        };
            ////    });


            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ////ValidateIssuer = true,
                   ////ValidateAudience = true,
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,

                   
                   ////ValidIssuer = "http://localhost:5000",
                   ////ValidAudience = "http://localhost:5000",
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTAuth:Secret"]))
               };
            });

            return services;
        }
    }
}