namespace XUnitCompleteExample.Query;

[Collection("TestCollectionFixture")]
public class UserQueryTest : QueryTestBase, IClassFixture<UserQueryFixture>
{
    protected readonly UserQueryFixture _fixture;

    public UserQueryTest(
        TestFixture testFixture,
        UserQueryFixture fixture,
        ITestOutputHelper testOutputHelper
        ) : base(testFixture, testOutputHelper)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Test()
    {
        //TODO. Start.
        //Substitute ADbContext with class name of db context of project you want to test.
        // var user = await _testFixture.ADbContext.Timbrature.FirstOrDefaultAsync();
        _testOutputHelper.WriteLine("Ok.");
    }
}