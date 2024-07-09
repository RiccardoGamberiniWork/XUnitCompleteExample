namespace XUnitCompleteExample;

public class TestBase
{
    public static readonly string _projectRootFolderPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
    public static readonly string _jsonFileFormat = nameof(FileFormats.Json).ToLower();
    public static readonly JsonSerializerSettings _jsonSettings = new() { Converters = { new StringEnumConverter() } };
    protected readonly ITestOutputHelper _testOutputHelper;

    public TestBase(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    public static string CreateFilePath(string fileNameWithoutExtension, string folderName = null)
    {
        string filePath;
        if (folderName != null)
        {
            string folderPath = Directory.GetDirectories(_projectRootFolderPath, folderName, SearchOption.AllDirectories)[0];
            filePath = $"{folderPath}\\{fileNameWithoutExtension}.{_jsonFileFormat}";
        }
        else
        {
            filePath = Directory.GetFiles(_projectRootFolderPath, $"{fileNameWithoutExtension}.{_jsonFileFormat}", SearchOption.AllDirectories)[0];
        }
        
        return filePath;
    }
    
    public static T MockJsonFile<T>(string fileNameWithoutExtension, string folderName = null)
    {
        string filePath = CreateFilePath(fileNameWithoutExtension, folderName);
        string objectJson = File.ReadAllText(filePath);
        T mockedObject = JsonConvert.DeserializeObject<T>(objectJson);
        return mockedObject;
    }
    
    public static T GetConfig<T>(string configFileName, string? sectionName = null)
    {
        string appSettingsFilePath = $"{_projectRootFolderPath}\\ConfigurationJsonFiles\\{configFileName}.json";
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddJsonFile(appSettingsFilePath, optional: true, reloadOnChange: true);
        
        if (sectionName != null)
            return GetSection<T>(configurationBuilder, sectionName);
        
        return configurationBuilder.Build().Get<T>();
    }
    
    public static SqlConnection CreateDbConn(string serverName, string dbName)
    {
        DbConnStr dbConnStr = GetConfig<DbConnStr>($"DbConnStr-{dbName}-{serverName}");
        var sqlConnection = new SqlConnection($"data source={dbConnStr.DataSource};" +
                                              $"User ID={dbConnStr.UserID};" +
                                              $"Password={dbConnStr.Password};" +
                                              $"initial catalog={dbConnStr.InitialCatalog};" +
                                              $"Encrypt={dbConnStr.Encrypt};" +
                                              $"TrustServerCertificate={dbConnStr.TrustServerCertificate};" +
                                              $"Trusted_Connection={dbConnStr.TrustedConnection};" +
                                              $"MultipleActiveResultSets={dbConnStr.MultipleActiveResultSets}");
        sqlConnection.Open();

        return sqlConnection;
    }
    
    public static T CreateDbContext<T>(SqlConnection sqlConnection, Func<DbContextOptions<T>, T> dbContextFactory) where T : DbContext
    {
        var options = new DbContextOptionsBuilder<T>().UseSqlServer(sqlConnection).Options;
        
        return dbContextFactory(options);
    }
    
    public static async Task<string> Authenticate(Credentials credentials, HttpClient httpClient)
    {
        string json = JsonConvert.SerializeObject(credentials, _jsonSettings);
        var body = new StringContent(json);
        var contentType = MediaTypeNames.Application.Json;
        body.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        HttpResponseMessage response = await httpClient.PostAsync($"{httpClient.BaseAddress}auth/autenticautente", body);
        string bodyJson = await response.Content.ReadAsStringAsync();
        UserContextDto contestoUtenteDto = JsonConvert.DeserializeObject<UserContextDto>(bodyJson);
        
        return contestoUtenteDto.SecureToken.Token;
    }

    private static T GetSection<T>(IConfigurationBuilder configurationBuilder, string sectionName)
    {
        return configurationBuilder.Build().GetSection(sectionName).Get<T>();
    }
    
    public static void ConfigureHttpClient(HttpClient httpClient, string configFileName)
    {
        HttpClientConfig httpClientConfig = GetConfig<HttpClientConfig>(configFileName);
        UriBuilder authUriPartBuilder = new()
        {
            Scheme = httpClientConfig.CommonScheme,
            Host = httpClientConfig.CommonHost,
            Port = httpClientConfig.CommonPort,
            Path = httpClientConfig.CommonPath
        };
        httpClient.BaseAddress = authUriPartBuilder.Uri;
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
    
    public void PrintJson(string bodyJson)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var jsonElement = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(bodyJson);
        _testOutputHelper.WriteLine(System.Text.Json.JsonSerializer.Serialize(jsonElement, options));
    }
    
    public void FailTest(string msg = "TEST FAILED", [CallerLineNumber] int lineNumber = 0)
    {
        _testOutputHelper.WriteLine($"{msg} =====> {lineNumber}");
        Assert.True(false);
    }
    
    #region  Yaml.

    public enum enumLogLevel
    {
        Information = 0,
        Debug = 100,
        Warning = 200,
        Err = 300,
        None = 400
    }

    public enum enumFileFormat
    {
        txt = 0,
        yaml = 100
    }
    
    private static string CreateFolderPath(List<string> folderPathList)
    {
        var rootFolderPath = @"C:\source\repos\XUnitCompleteExample";
        var folderPath = rootFolderPath + @"\";

        foreach (var folderName in folderPathList)
            folderPath = folderPath + folderName + @"\";

        // Removes last character from file path string.
        return folderPath.Substring(0, folderPath.Length - 1);
    }

    private static string GetFilePath(string fileName, enumFileFormat fileFormat = enumFileFormat.txt)
    {
        try
        {
            var rootFolderPath = @"C:\source\repos\XUnitCompleteExample";
            var fileSearchResult = Directory.GetFiles(rootFolderPath, $"{fileName}.{fileFormat.ToString()}", SearchOption.AllDirectories);

            if (fileSearchResult.Length > 1)
            {
                var ex = new Exception($"More than one file with name {fileName} has been found.");
                //Log(msg: "An error occurred while GetFilePath.", lvl: enumLogLevel.Err, exMsg: ex.Message);
                throw ex;
            }

            if (fileSearchResult.Length == 0
               )
                return null;
            
            return fileSearchResult[0];
        }
        catch (Exception ex)
        {
            //Log(msg: "An error occurred while DeleteFile.", lvl: enumLogLevel.Err, exMsg: ex.Message);
            throw ex;
        }
    }

    public static void DeleteFile(string fileName, enumFileFormat fileFormat = enumFileFormat.txt, List<string> folderPathList = null)
    {
        try
        {
            string filePath;

            if (folderPathList == null)
                filePath = GetFilePath(fileName, fileFormat);
            else
            {
                if (folderPathList.Count == 0)
                    throw new Exception("The list of folders names is empty.");

                var folderPath = CreateFolderPath(folderPathList);
                filePath = $@"{folderPath}\{fileName}.{fileFormat.ToString()}";
            }

            if (filePath == null)
                return;

            File.Delete(filePath);
        }
        catch (Exception ex)
        {
            //Log(msg: "An error occurred while DeleteFile.", lvl: enumLogLevel.Err, exMsg: ex.Message);
            throw;
        }
    }

    public static void CreateFile(string fileName, enumFileFormat fileFormat = enumFileFormat.txt, List<string> folderPathList = null)
    {
        try
        {
            if (folderPathList == null)
                folderPathList = new List<string>() { fileFormat.ToString() };

            if (folderPathList.Count == 0)
                throw new Exception("The list of folders names is empty.");

            var folderPath = CreateFolderPath(folderPathList);

            if (!Directory.Exists(folderPath))
                CreateFolder(folderPathList);

            var filePath = $@"{folderPath}\{fileName}.{fileFormat.ToString()}";

            if (File.Exists(filePath))
                File.Delete(filePath);

            var sw = new StreamWriter(filePath, true);
            sw.WriteLine("");
            sw.Close();
        }
        catch (Exception ex)
        {
            //Log(msg: "An error occurred while CreateFile.", lvl: enumLogLevel.Err, exMsg: ex.Message);
            throw;
        }
    }

    public static void CreateYaml(string fileName, List<string> folderPathList = null)
    {
        try
        {
            if (folderPathList == null)
                folderPathList = new List<string> { "yaml" };

            if (folderPathList.Count == 0)
                throw new Exception("The list of folders names is empty.");

            var folderPath = CreateFolderPath(folderPathList);

            if (!Directory.Exists(folderPath))
                CreateFolder(folderPathList);

            var filePath = $@"{folderPath}\{fileName}.{enumFileFormat.yaml.ToString()}";

            if (File.Exists(filePath))
                File.Delete(filePath);

            var serializer = new SerializerBuilder().Build();
            var dict = new Dictionary<object, object> { { "Name", fileName } };
            string yaml = serializer.Serialize(dict);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(yaml);
                    streamWriter.Close();
                }

                fileStream.Close();
            }
        }
        catch (Exception ex)
        {
            //Log(msg: "An error occurred while CreateYaml.", lvl: enumLogLevel.Err, exMsg: ex.Message);
            throw;
        }
    }
    
    public static Dictionary<object, object> D(object obj)
    {
        return (Dictionary<object, object>) obj;
    }

    public static Dictionary<object, object> GetYaml(string str)
    {
        try
        {
            string filePath;
            filePath = str;

            if (!str.Contains(@"\"))
            {
                filePath = GetFilePath(str, enumFileFormat.yaml);
                if (filePath == null)
                    throw new Exception($"File {filePath} doesn't exists.");
            }

            string yamlContent = File.ReadAllText(filePath);
            var deserializer = new DeserializerBuilder().Build();
            var dict = deserializer.Deserialize<Dictionary<object, object>>(yamlContent);

            return dict;
        }
        catch (Exception ex)
        {
            //Log(msg: "An error occurred while GetYaml", lvl: enumLogLevel.Err, exMsg: ex.Message);
            throw ex;
        }
    }

    public static void UpdateYaml(Dictionary<object, object> dict, string fileName)
    {
        try
        {
            var filePath = GetFilePath(fileName, enumFileFormat.yaml);

            if (filePath == null)
                throw new Exception($"File {filePath} doesn't exists.");

            var serializer = new SerializerBuilder().Build();
            string yaml = serializer.Serialize(dict);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(yaml);
                    streamWriter.Close();
                }

                fileStream.Close();
            }
        }
        catch (Exception ex)
        {
            //Log(msg: "An error occurred while UpdateYaml.", lvl: enumLogLevel.Err, exMsg: ex.Message);
            throw ex;
        }
    }

    // public static Dictionary<object, object> CreateDict(object items)
    // {
    //     return Enumerable.Range(0, items.Length / 2).ToDictionary(i => items(i * 2), i => items(i * 2 + 1));
    // }

    public static void DeleteFolder(List<string> folderPathList)
    {
        try
        {
            if (folderPathList.Count == 0)
                throw new Exception("The list of folders names is empty.");

            var folderPath = CreateFolderPath(folderPathList);

            if (Directory.Exists(folderPath))
                Directory.Delete(folderPath, true);
        }
        catch (Exception ex)
        {
            //Log(msg: "An error occurred while DeleteFolder.", lvl: enumLogLevel.Err, exMsg: ex.Message);
            throw ex;
        }
    }

    // The argument of this function contains folder names that have a hierarchy. They are nested. Example of the list: ["alpha", "beta (nested_in_alpha)", "gamma (nested in beta)"].
    public static void CreateFolder(List<string> folderPathList)
    {
        try
        {
            if (folderPathList.Count == 0)
                throw new Exception("The list of folders names is empty.");

            var folderPath = CreateFolderPath(folderPathList);

            if (Directory.Exists(folderPath))
                Directory.Delete(folderPath, true);

            var rootFolderPath = @"C:\source\repos\XUnitCompleteExample";

            folderPath = rootFolderPath;

            foreach (var folderName in folderPathList)
            {
                if (!Directory.Exists($@"{folderPath}\{folderName}"))
                    Directory.CreateDirectory($@"{folderPath}\{folderName}");

                folderPath = $@"{folderPath}\{folderName}\";
            }
        }
        catch (Exception ex)
        {
            //Log(msg: "An error occurred while CreateFolder.", lvl: enumLogLevel.Err, exMsg: ex.Message);
            throw ex;
        }
    }

    public static string ObjToJson(object item)
    {
        return JsonConvert.SerializeObject(item);
    }
    
    #endregion
}