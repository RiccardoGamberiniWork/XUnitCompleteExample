namespace XUnitCompleteExample.Identity.Serializers;

public class SerializerJson
{
    private static readonly string _testProjectRootFolderPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
    private static readonly string _testProjectJsonsFolderPath = $"{_testProjectRootFolderPath}\\{Assembly.GetExecutingAssembly().GetName().Name}.Tests\\Jsons";
    private static readonly string _jsonFileFormat = nameof(FileFormats.Json).ToLower();
    
    // private static string CreateFilePath(string fileNameWithoutExtension, string[] foldersTree)
    // {
    //     string filePath = _testProjectJsonsFolderPath;
    //     string currentFolderPath = String.Empty;
    //     foreach (string folderName in foldersTree)
    //     {
    //         filePath += $"\\{folderName}\\";
    //         currentFolderPath = $"{_testProjectJsonsFolderPath}\\{folderName}";
    //         if (Directory.Exists(currentFolderPath))
    //             continue;
    //         Directory.CreateDirectory(currentFolderPath);
    //         
    //     }
    //     filePath += $"{fileNameWithoutExtension}.{_jsonFileFormat}";
    //     return filePath;
    // }
    
    private static string CreateFilePath(string fileNameWithoutExtension, string[] foldersTree)
    {
        string partialFilePath = _testProjectJsonsFolderPath;
        foreach (string folderName in foldersTree)
        {
            partialFilePath += $"\\{folderName}";
            if (!Directory.Exists(partialFilePath))
                Directory.CreateDirectory(partialFilePath);
        }
        return $"{partialFilePath}\\{fileNameWithoutExtension}.{_jsonFileFormat}";
    }
    
    public static void CreateJsonFile<T>(T obj, string fileNameWithoutExtension, string[] foldersTree)
    {
        string filePath = CreateFilePath(fileNameWithoutExtension, foldersTree);
        string json = JsonConvert.SerializeObject(obj);
        var options = new JsonSerializerOptions { WriteIndented = true };
        var jsonElement = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(json);
        File.WriteAllText(filePath, System.Text.Json.JsonSerializer.Serialize(jsonElement, options));
    }

    public static void CreateJsonFiles()
    {
        // TODO.
        // When CreateJsonFiles is executed a set of objects are serialized in json files.
        // When serialized objects classes are modified you can modify objects definition in CreateJsonFiles function and then create again all json files.
        
        // User.
        var user = new User
        {
            Id = 1,
            Username = "RiccardoGamberini",
            Name = "Riccardo",
            Surname = "Gamberini",
            PasswordHash = new byte[] { 0x01, 0x02, 0x03 },
            Salt = new byte[] { 0x04, 0x05, 0x06 }
        };
        CreateJsonFile(user, "User", new string[] { "Models" });
        // CreateJsonFile(id, "UserId", new[] {"User"});
        // CreateJsonFile(user, "User", new[] {"User"});
        // CreateJsonFile(_jsonResponseFormatService.Error(msg: _texts.UserGenericError, statusCode: StatusCodes.Status500InternalServerError), "StringInternalServerErrorResponse", new[] {"Common"});
        // CreateJsonFile(userDto, "UserDto", new[] {"User", "UserController", "GetById"});
        // CreateJsonFile(response, "InformationBadRequestResponse", new[] {"Common"});
        // CreateJsonFile(response, "ListErrorDtoBadRequestResponse", new[] {"Common"});
        // CreateJsonFile(addUserDto, "AddUserDto", new[] {"User", "UserController", "Add"});
        // CreateJsonFile(response, "ErrorInternalServerErrorResponse", new[] {"Common"});
        // CreateJsonFile(_jsonResponseFormatService.Success(obj: $"User {user.Username} added successfully.", statusCode: StatusCodes.Status201Created), "StringOKResponse", new[] {"Common"});
    }
}