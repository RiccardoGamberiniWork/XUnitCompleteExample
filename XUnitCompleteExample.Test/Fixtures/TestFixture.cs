using XUnitCompleteExample.Models.Enums;

namespace XUnitCompleteExample.Fixtures;

public class TestFixture : IAsyncLifetime
{
    //TODO. Add mapper in TestFixture.
    // private readonly IMapper _mapper;
    private readonly HttpClient _authHttpClient;
    private readonly HttpClient _httpClient;
    //Hardcoded ids.
    private readonly long _anId = 1;

    public long AnId => _anId;
    public string DefaultApiVersion { get; set; } = "1.0.0";
    public JsonSerializerSettings JsonSettings { get; set; }
    public HttpClient AuthHttpClient { get; set; }
    public HttpClient HttpClient { get; set; }
    public string JsonFileFormat { get; set; }
    public Credentials Credentials { get; set; } = new() { Username = "aUsername", Password = "aPassword" };
    //TODO.
    //Read TODO titled "Add mapper in TestFixture".
    // public IMapper Mapper => _mapper;
    //TODO. Start.
    //Substitute ADbContext with class name of db context of project you want to test.
    // public ADbContext ADbContext { get; set; }
    public SqlConnection SqlConnection { get; set; }

    public TestFixture()
    {
        JsonSettings = new JsonSerializerSettings { Converters = { new StringEnumConverter() } };
        JsonFileFormat = nameof(FileFormats.Json).ToLower();
        AuthHttpClient = new HttpClient();
        TestBase.ConfigureHttpClient(AuthHttpClient, "AuthHttpClientConfigLocalhost");
        HttpClient = new HttpClient();
        TestBase.ConfigureHttpClient(HttpClient, "HttpClientConfigLocalhost");
        //TODO.
        //Read TODO titled "Add mapper in TestFixture".
        // _mapper = CreateMapper();
        SqlConnection = TestBase.CreateDbConn("aServerName", "aDatabaseName");
        //Create a default database context.
        //TODO. Start.
        //Substitute ADbContext with class name of db context of project you want to test.
        // TigerDbContext = TestBase.CreateDbContext<ADbContext>(SqlConnection, options => new ADbContext(options));
    }
    
    public async Task InitializeAsync()
    {
        string JWTToken = await TestBase.Authenticate(Credentials, AuthHttpClient);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(nameof(TokenTypes.Bearer), JWTToken);
    }
    
    public async Task UseUser(string username)
    {
        var httpClient = new HttpClient();
        TestBase.ConfigureHttpClient(httpClient, "HttpClientConfigLocalhost");
        Credentials = TestBase.MockJsonFile<Credentials>(username);
        string JWTToken = await TestBase.Authenticate(Credentials, AuthHttpClient);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(nameof(TokenTypes.Bearer), JWTToken);
    }
    
    // public async Task SetDefaultUtente(string username)
    // {
    //     Credentials = TestBase.MockJsonFile<Credentials>(username);
    //     string JWTToken = await TestBase.Authenticate(Credentials, AuthHttpClient);
    //     HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(nameof(TokenTypes.Bearer), JWTToken);
    // }
    
    // public void SetDefaultDbContext<T>(string serverName, string dbName) where T : DbContext
    // {
    //     SqlConnection = TestBase.CreateDbConn(serverName, dbName);
    //     DbContext = TestBase.CreateDbContext<T>(SqlConnection, options => new T(options));
    // }
    
    // public async Task<T> UseDbContext<T>(string serverName, string dbName)
    // {
    //     SqlConnection = TestBase.CreateDbConn(serverName, dbName);
    //     return TestBase.CreateDbContext<T>(SqlConnection, options => new TigerDbContext(options));
    // }
    
    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
    
    //TODO.
    //Read TODO titled "Add mapper in TestFixture".
    // private IMapper CreateMapper()
    // {
    //     var services = new ServiceCollection();
    //     services.AddAutoMapper(cfg =>
    //         {
    //             cfg.AddProfile<IntercomProfile>();
    //         },
    //         AppDomain.CurrentDomain.GetAssemblies());
    //     var serviceProvider = services.BuildServiceProvider();
    //     
    //     return serviceProvider.GetService<IMapper>();
    // }
}