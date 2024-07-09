namespace XUnitCompleteExample.Identity.Extensions
{
    public static class ValidationServiceExtensions
    {
        public static void ValidUsername<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder.NotEmpty();
            ruleBuilder.Custom((username, context) =>
            {
                if (username.Contains(' '))
                    context.AddFailure("'Username' non può contenere spazi.");
            });
            ruleBuilder.MaximumLength(25);
        }
        
        public static void Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder.NotEmpty();
            ruleBuilder.Custom((username, context) =>
            {
                if (username.Contains(' '))
                    context.AddFailure("'Username' non può contenere spazi.");
            });
            ruleBuilder.MaximumLength(25);
        }

        public static IServiceCollection AddValidatorServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<long>, IdValidator>();
            services.AddScoped<IValidator<AddUserDto>, AddUserDtoValidator>();
            services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
            return services;
        }
    }
}