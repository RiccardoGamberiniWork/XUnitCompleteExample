//
string configurationFilesFolderName = "ConfigurationJsonFiles";
string configurationFileNamePrefix = "appsettings";

try
{
    Log.Information("Application is starting.");
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddCommandLine(args);
    // Verifichiamo se l'applicazione è stata lanciata usando l'opzione --config.
    string configurationName = builder.Configuration["config"] == null || builder.Configuration["config"] == "" ? "Development" : builder.Configuration["config"];
    string configurationFileName = $"{configurationFileNamePrefix}.{configurationName}.{nameof(FileFormats.Json)}";
    string configurationFilePath = $"{configurationFilesFolderName}/{configurationFileName}";
    if (!File.Exists(configurationFilePath))
    {
        Log.Fatal($"{configurationFilePath} file not found.");
        Environment.Exit(-1);
    }
    builder.Configuration.AddJsonFile(configurationFilePath, optional: false, reloadOnChange: true);
    Console.Title = $"XUnitCompleteExample Identity - Configuration: {configurationName}";
    
    builder.Host.UseSerilog((context, loggerConfiguration) =>
    {
        // Le diverse configurazioni di Serilog per i diversi ambienti dell'applicazione (development, production eccetera) sono
        // contenute nella sezione "Serilog" dei file della cartella "ConfigurationJsonFiles". La proprietà "Configuration"
        // della variabile context è la configurazione (per esempio il file appsettings.Local.json) usata dall'applicazione.
        loggerConfiguration.ReadFrom.Configuration(context.Configuration);
    });

    builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {
            options.FormatInvalidModelStateResponse();
        })
        .AddNewtonsoftJson(options => {
            options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        })
        .AddXmlSerializerFormatters();
    builder.Services.AddSingleton<ILogService, LogService>();
    builder.Services.AddTransient<IJsonResponseFormatService, JsonResponseFormatService>();
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddIdentityServices(builder.Configuration);
    builder.Services.AddRepositoryServices();
    builder.Services.AddSecurityServices();
    builder.Services.AddValidatorServices();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    var app = builder.Build();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(swaggerUIOptions =>
        {
            // Siccome è possible usare più di una versione dell'API contemporaneamente, modifichiamo la UI di Swagger in modo che gestisca più di una versione.
            foreach (ApiVersionDescription apiVersionDescription in app.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
            {
                swaggerUIOptions.SwaggerEndpoint($"/swagger/{apiVersionDescription.GroupName}/swagger.json", $"XUnitCompleteExample Identity Api {apiVersionDescription.GroupName.ToUpperInvariant()}");
            }
            swaggerUIOptions.RoutePrefix = string.Empty;
        });
    }

    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();// Abilita la scrittura automatica di log strutturati relativi alle richieste HTTP.
    app.UseRouting();

    // app.UseCors(x => x.WithOrigins("https://localhost:44347/")); //"sslPort": 44320
    // global cors policy
    app.UseCors(x => x.WithOrigins()
        .AllowAnyMethod()
        .AllowAnyHeader());

    app.UseAuthentication();
    app.UseAuthorization();
    
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start.");
}
finally
{
    Log.CloseAndFlush();
}




