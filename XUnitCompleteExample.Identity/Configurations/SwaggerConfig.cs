namespace XUnitCompleteExample.Identity.Configurations
{
    public class SwaggerConfig : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _provider;
        
        public SwaggerConfig(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(SwaggerGenOptions swaggerGenOptions)
        {
            // Evitiamo che swagger produca un documento per ogni versione delle API. Per farlo creaiamo dei gruppi in un stesso documento.
            foreach (var apiVersionDescription in _provider.ApiVersionDescriptions)
            {
                swaggerGenOptions.SwaggerDoc(apiVersionDescription.GroupName, new OpenApiInfo
                {
                    Title = $"XUnitCompleteExample identity service {apiVersionDescription.ApiVersion}",
                    Version = apiVersionDescription.ApiVersion.ToString()
                });
            }
            // Imposta la lettura da parte di Swagger UI della documentazione in formato XML generata automaticamente da ASP.NET Core.
            // Se Swagger UI legge questa documentazione possiamo modificare i contenuti visualizzati da Swagger UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            swaggerGenOptions.IncludeXmlComments(xmlPath);
        }
    }
}
