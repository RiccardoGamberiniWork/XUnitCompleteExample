using XUnitCompleteExample.Fixtures;

namespace XUnitCompleteExample;

// TODO.
// Cercare di spostare tutti i metodi di questa class in TestHelper.
// Questa classe deve contenere tutti i test comuni alle classi figlie.
public class ControllerTestBase : TestBase
{
    // protected readonly TestFixture _testFixture;
    //
    // public ControllerTestBase(TestFixture testFixture, ITestOutputHelper testOutputHelper) : base (testOutputHelper)
    // {
    //     _testFixture = testFixture;
    // }
    //
    // // private string AddRouteTemplate(string url, string routeTemplate)
    // // {
    // //     // Esempio di className: Utente.
    // //     string className = GetType().Name.Split("Controller").First();
    // //     // Esempio di path in questo else: getAll.
    // //     // Esempio id url dopo l'interpolazione di stringhe: https://localhost:5000/api/v1/Utente/getAll.
    // //     return $"{url}/{className}/{routeTemplate}";
    // // }
    // //
    // // private string AddApiVersion(string url, string apiVersion)
    // // {
    // //     string apiMajorVersion = apiVersion.Split('.').First();
    // //     // Esempio: https://localhost:5000/api/v1
    // //     return $"{url}v{apiMajorVersion}";
    // // }
    //
    // // public string CreateUrl(string path, bool isRouteTemplate = true)
    // // {
    // //     string url = AddApiVersion(_testFixture.HttpClient.BaseAddress.ToString(), _testFixture.DefaultApiVersion);
    // //     return isRouteTemplate ? AddRouteTemplate(url, path) : $"{url}/{path}";
    // // }
    // //
    // // public string CreateUrl(string path, Dictionary<string, string> queryParameters, bool pathIsRouteTemplate = true)
    // // {
    // //     string url = AddApiVersion(_testFixture.HttpClient.BaseAddress.ToString(), _testFixture.DefaultApiVersion);
    // //     url = pathIsRouteTemplate ? AddRouteTemplate(url, path) : $"{url}/{path}";
    // //     return QueryHelpers.AddQueryString(url, queryParameters);
    // // }
    //
    // // public string CreateUrl(string apiVersion, string path, bool pathIsRouteTemplate = true)
    // // {
    // //     string url = AddApiVersion(_testFixture.HttpClient.BaseAddress.ToString(), apiVersion);
    // //     return pathIsRouteTemplate ? AddRouteTemplate(url, path) : $"{url}/{path}";
    // // }
    //
    // // public string CreateUrl(string apiVersion, string path, Dictionary<string, string> queryParameters, bool pathIsRouteTemplate = true)
    // // {
    // //     string url = AddApiVersion(_testFixture.HttpClient.BaseAddress.ToString(), apiVersion);
    // //     url = pathIsRouteTemplate ? AddRouteTemplate(url, path) : $"{url}/{path}";
    // //     return QueryHelpers.AddQueryString(url, queryParameters);
    // // }
    //
    // // public string CreateUrlWith(string path, string apiVersion, Dictionary<string, string>? queryParameters = null, bool isRouteTemplate = true)
    // // {
    // //     string? apiMajorVersion = apiVersion?.Split('.').First() ?? _testFixture.DefaultApiVersion?.Split('.').First();
    // //     // Esempio: https://localhost:5000/api/v1
    // //     string url = $"{_testFixture.HttpClient.BaseAddress}v{apiMajorVersion}";
    // //         
    // //     if(!isRouteTemplate)
    // //     {
    // //         // Esempio di path in questo if: Auth/AutenticaUtente.
    // //         // Esempio id url dopo l'interpolazione di stringhe: https://localhost:5000/api/v1/Auth/AutenticaUtente.
    // //         url = $"{url}/{path}";
    // //     } else
    // //     {
    // //         // Esempio di className: Utente.
    // //         string className = GetType().Name.Split("Controller").First();
    // //         // Esempio di path in questo else: getAll.
    // //         // Esempio id url dopo l'interpolazione di stringhe: https://localhost:5000/api/v1/Utente/getAll.
    // //         url = $"{url}/{className}/{path}";
    // //     }
    // //         
    // //     if (queryParameters != null) url = QueryHelpers.AddQueryString(url, queryParameters);
    // //         
    // //     return url;
    // // }
    //
    // public string CreateUri(string path, bool isEntirePath = false, Dictionary<string, string> queryParameters = null)
    // {
    //     string uri;
    //     if(isEntirePath)
    //     {
    //         uri = $"{_testFixture.HttpClient.BaseAddress}{path}";
    //     } else
    //     {
    //         string commonPartialPath = GetType().Name.Replace("ControllerTests", "");
    //         uri = $"{_testFixture.HttpClient.BaseAddress}{commonPartialPath}/{path}";
    //     }
    //     string uriToCall = queryParameters == null ? uri : QueryHelpers.AddQueryString(uri, queryParameters);
    //     return uriToCall;
    // }
    //
    // public void PrintExpectedActual(Dictionary<string, KeyValuePair<string, string>> values)
    // {
    //     foreach (KeyValuePair<string, KeyValuePair<string, string>> kVP in values)
    //     {
    //         _testOutputHelper.WriteLine(kVP.Key);
    //         _testOutputHelper.WriteLine($"- Expected: {kVP.Value.Key}");
    //         _testOutputHelper.WriteLine($"- Actual: {kVP.Value.Value}");
    //     }
    // }
    //
    // public StringContent CreateBody<T>(T item, string contentType = MediaTypeNames.Application.Json)
    // {
    //     string json = JsonConvert.SerializeObject(item, _testFixture.JsonSettings);
    //     var body = new StringContent(json);
    //     body.Headers.ContentType = new MediaTypeHeaderValue(contentType);
    //         
    //     return body;
    // }
    
    protected readonly TestFixture _testFixture;

    public ControllerTestBase(TestFixture testFixture, ITestOutputHelper testOutputHelper) : base (testOutputHelper)
    {
        _testFixture = testFixture;
    }
    
    private string AddRouteTemplate(string url, string routeTemplate)
    {
        // Esempio di className: Utente.
        string className = GetType().Name.Split("Controller").First();
        // Esempio di path in questo else: getAll.
        // Esempio id url dopo l'interpolazione di stringhe: https://localhost:5000/api/v1/Utente/getAll.
        return $"{url}{className}/{routeTemplate}";
    }
    
    private string AddApiVersion(string url, string apiVersion)
    {
        string apiMajorVersion = apiVersion.Split('.').First();
        // Esempio: https://localhost:5000/api/v1
        return $"{url}v{apiMajorVersion}";
    }
    
    public string CreateUrl(string path, bool isRouteTemplate = true)
    {
        string url = AddApiVersion(_testFixture.HttpClient.BaseAddress.ToString(), _testFixture.DefaultApiVersion);
        return isRouteTemplate ? AddRouteTemplate(url, path) : $"{url}/{path}";
    }
    
    public string CreateUrlNoAPIVersion(string path, bool pathIsRouteTemplate = true)
    {
        string url = _testFixture.HttpClient.BaseAddress.ToString();
        return pathIsRouteTemplate ? AddRouteTemplate(url, path) : $"{url}/{path}";
    }
    
    public string CreateUrl(string path, Dictionary<string, string> queryParameters, bool pathIsRouteTemplate = true)
    {
        string url = AddApiVersion(_testFixture.HttpClient.BaseAddress.ToString(), _testFixture.DefaultApiVersion);
        url = pathIsRouteTemplate ? AddRouteTemplate(url, path) : $"{url}/{path}";
        return QueryHelpers.AddQueryString(url, queryParameters);
    }
    
    public string CreateUrlNoAPIVersion(string path, Dictionary<string, string> queryParameters, bool pathIsRouteTemplate = true)
    {
        string url = _testFixture.HttpClient.BaseAddress.ToString();
        url = pathIsRouteTemplate ? AddRouteTemplate(url, path) : $"{url}/{path}";
        return QueryHelpers.AddQueryString(url, queryParameters);
    }

    public string CreateUrl(string apiVersion, string path, bool pathIsRouteTemplate = true)
    {
        string url = AddApiVersion(_testFixture.HttpClient.BaseAddress.ToString(), apiVersion);
        return pathIsRouteTemplate ? AddRouteTemplate(url, path) : $"{url}/{path}";
    }
    
    public string CreateUrl(string apiVersion, string path, Dictionary<string, string> queryParameters, bool pathIsRouteTemplate = true)
    {
        string url = AddApiVersion(_testFixture.HttpClient.BaseAddress.ToString(), apiVersion);
        url = pathIsRouteTemplate ? AddRouteTemplate(url, path) : $"{url}/{path}";
        return QueryHelpers.AddQueryString(url, queryParameters);
    }
    
    public static string CreateUrl(string path, string[] parameters)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat(path, parameters);
        return stringBuilder.ToString();
    }

    // public string CreateUrlWith(string path, string apiVersion, Dictionary<string, string>? queryParameters = null, bool isRouteTemplate = true)
    // {
    //     string? apiMajorVersion = apiVersion?.Split('.').First() ?? _testFixture.DefaultApiVersion?.Split('.').First();
    //     // Esempio: https://localhost:5000/api/v1
    //     string url = $"{_testFixture.HttpClient.BaseAddress}v{apiMajorVersion}";
    //         
    //     if(!isRouteTemplate)
    //     {
    //         // Esempio di path in questo if: Auth/AutenticaUtente.
    //         // Esempio id url dopo l'interpolazione di stringhe: https://localhost:5000/api/v1/Auth/AutenticaUtente.
    //         url = $"{url}/{path}";
    //     } else
    //     {
    //         // Esempio di className: Utente.
    //         string className = GetType().Name.Split("Controller").First();
    //         // Esempio di path in questo else: getAll.
    //         // Esempio id url dopo l'interpolazione di stringhe: https://localhost:5000/api/v1/Utente/getAll.
    //         url = $"{url}/{className}/{path}";
    //     }
    //         
    //     if (queryParameters != null) url = QueryHelpers.AddQueryString(url, queryParameters);
    //         
    //     return url;
    // }
    
    public string CreateUri(string path, bool isEntirePath = false, Dictionary<string, string> queryParameters = null)
    {
        string uri;
        if(isEntirePath)
        {
            uri = $"{_testFixture.HttpClient.BaseAddress}{path}";
        } else
        {
            string commonPartialPath = GetType().Name.Replace("ControllerTests", "");
            uri = $"{_testFixture.HttpClient.BaseAddress}{commonPartialPath}/{path}";
        }
        string uriToCall = queryParameters == null ? uri : QueryHelpers.AddQueryString(uri, queryParameters);
        return uriToCall;
    }
    
    public void PrintExpectedActual(Dictionary<string, KeyValuePair<string, string>> values)
    {
        foreach (KeyValuePair<string, KeyValuePair<string, string>> kVP in values)
        {
            _testOutputHelper.WriteLine(kVP.Key);
            _testOutputHelper.WriteLine($"- Expected: {kVP.Value.Key}");
            _testOutputHelper.WriteLine($"- Actual: {kVP.Value.Value}");
        }
    }
    
    public StringContent CreateBody<T>(T item, string contentType = MediaTypeNames.Application.Json)
    {
        string json = JsonConvert.SerializeObject(item, _testFixture.JsonSettings);
        var body = new StringContent(json);
        body.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            
        return body;
    }
    
    public bool CheckStatusCode(HttpResponseMessage actualResponse, int expectedStatusCode)
    {
        return (int) actualResponse.StatusCode == expectedStatusCode;
    }
    
    public async Task CloseConnectionAndFailTest(string msg = "TEST FAILED", [CallerLineNumber] int lineNumber = 0)
    {
        await _testFixture.SqlConnection.CloseAsync();
        _testOutputHelper.WriteLine($"{msg} =====> {lineNumber}");
        Assert.True(false);
    }
}