namespace XUnitCompleteExample.Example;

[Collection("TestCollectionFixture")]
public class UserControllerTest : ControllerTestBase, IClassFixture<UserControllerFixture>
{
    protected readonly UserControllerFixture _fixture;

    public UserControllerTest(
        TestFixture testFixture,
        UserControllerFixture fixture,
        ITestOutputHelper testOutputHelper
        ) : base(testFixture, testOutputHelper)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Login_Example()
    {
        await _testFixture.UseUser("aUsername");
    }
    
    [Fact]
    public async Task DbContext_Example()
    {
        // var sqlConnection = CreateDbConn("aServerName", "aDbName");
        // //TODO. Start.
        // //Substitute ADbContext with class name of db context of project you want to test.
        // var dbContext = CreateDbContext<ADbContext>(sqlConnection, options => new ADbContext(options));
        // //The following query is sent to db specified above.
        // var user = await dbContext.Users.Where(x =>
        //         x.Id.Equals(_testFixture.AnId)
        //     .SingleOrDefaultAsync());
    }
    
    [Fact]
    public async Task Yaml_Example()
    {
        var fileName = "yaml_example_file";
        CreateYaml(fileName, new List<string> { "yaml_example_first_folder", "yaml_example_second_folder", "yaml_example_third_folder" });
        var dictDcd = GetYaml(fileName);
        dictDcd["first_level"] = new Dictionary<object, object>();
        D(dictDcd["first_level"])["title"] = "first_title";
        D(dictDcd["first_level"])["second_level"] = new Dictionary<object, object>();
        D(D(dictDcd["first_level"])["second_level"])["title"] = "second_title";
        D(D(dictDcd["first_level"])["second_level"])["third_level"] = new Dictionary<object, object>();
        D(D(D(dictDcd["first_level"])["second_level"])["third_level"])["title"] = "third_title";
        UpdateYaml(dictDcd, fileName);
        DeleteFile(fileName, enumFileFormat.yaml);
    }
    
    [Fact]
    public async Task Get_Deserializes_GetUser_Should_Return_OK()
    {
        int OKStatusCode = StatusCodes.Status200OK;
        var queryParams = new Dictionary<string, string>
        {
            {"Id", "1"}
        };
        HttpResponseMessage response = await _testFixture.HttpClient.GetAsync(QueryHelpers.AddQueryString("http://localhost:5178/api/users/", queryParams));
        string responseJson = await response.Content.ReadAsStringAsync();
        bool isExpectedStatusCode = CheckStatusCode(response, StatusCodes.Status200OK);
        
        if (!isExpectedStatusCode)
        {
            var values = new Dictionary<string, KeyValuePair<string, string>>
            {
                {nameof(response.StatusCode), new KeyValuePair<string, string>(OKStatusCode.ToString(), ((int)response.StatusCode).ToString())},
            };
            PrintExpectedActual(values);
            await _testFixture.SqlConnection.CloseAsync();
            FailTest();
        }
        
        UserDto userDtoOut = JsonConvert.DeserializeObject<UserDto>(responseJson);
        // await CheckReturnedDtoGet(userDtoOut, queryParams);
        PrintJson(responseJson);
        //All tests passed.
        await _testFixture.SqlConnection.CloseAsync();
        Assert.True(true);
    }
    
    //TODO. Start.
    //UserDto is a class that XUnitCompleteExample must know through reference to another project.
    //Checks if returned dto is coherent with query parameters.
    // private async Task CheckReturnedDtoGet(UserDto userDtoGet, Dictionary<string, string> queryParams)
    // {
    //     if (!userDtoGet.Id.Equals(int.Parse(queryParams["Id"])))
    //     {
    //         await _testFixture.SqlConnection.CloseAsync();
    //         FailTest();
    //     }
    // }
    
    // private async Task<User> QueryExample(User userDtoOut)
    // {
    //     //TODO. Start.
    //     //Substitute ADbContext with class name of db context of project you want to test.
    //     var user = await _testFixture.ADbContext.Users.AsNoTracking().Where(x =>
    //             x.Id.Equals(1)
    //         .SingleOrDefaultAsync());
    //
    //     return user;
    // }
}