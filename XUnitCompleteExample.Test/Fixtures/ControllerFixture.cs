using XUnitCompleteExample.Configurations;
using XUnitCompleteExample.Models.Enums;

namespace XUnitCompleteExample.Fixtures;

// Tmp. Questa class contiene la creazione di un client HTTP. Grazie all'attributo [Collection("CollectionControllerFixture")] usato nella ControllerTestBase la stessa istanza del client HTTP viene usata
// in tutte le class che ereditano da ControllerTestBase.
public class ControllerFixture : IAsyncLifetime
{
    public string DefaultApiVersion { get; set; }
    public JsonSerializerSettings JsonSettings { get; set; }
    public HttpClient HttpClient { get; set; }
    public string ProjectRootFolderPath { get; set; }
    public string JsonFileFormat { get; set; }

    public async Task InitializeAsync()
    {
        HttpClientConfig httpClientConfig = GetHttpClientConfig();
        DefaultApiVersion = httpClientConfig.DefaultApiVersion;
        JsonSettings = new JsonSerializerSettings { Converters = { new StringEnumConverter() } };
        JsonFileFormat = nameof(FileFormats.Json).ToLower();
        HttpClient = new HttpClient();
        // Creo la parte di url comune a tutte le richieste HTTP fatte dai test.
        UriBuilder commonUriPartBuilder = new()
        {
            Scheme = httpClientConfig.CommonScheme,
            Host = httpClientConfig.CommonHost,
            Port = httpClientConfig.CommonPort,
            Path = httpClientConfig.CommonPath
        };
        HttpClient.BaseAddress = commonUriPartBuilder.Uri;
    }
    
    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
    
    private HttpClientConfig GetHttpClientConfig()
    {
        // string AspNetCoreTestsConfig = Environment.GetEnvironmentVariable("ASPNETCORE_TESTS_CONFIG");
        // if (AspNetCoreTestsConfig == null)
        //     throw new Exception("ASPNETCORE_TESTS_CONFIG environment variable not found.");
        // string appSettingsFilePath = $"{_projectRootFolderPath}\\ConfigurationJsonFiles\\HttpClientConfig{AspNetCoreTestsConfig}.json";
        ProjectRootFolderPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
        string appSettingsFilePath = $"{ProjectRootFolderPath}\\ConfigurationJsonFiles\\HttpClientConfigLocalhost.json";
        IConfigurationBuilder _configurationBuilder = new ConfigurationBuilder().AddJsonFile(appSettingsFilePath, optional: true, reloadOnChange: true);
    
        return _configurationBuilder.Build().GetSection("HttpClientConfig").Get<HttpClientConfig>();
    }
}
