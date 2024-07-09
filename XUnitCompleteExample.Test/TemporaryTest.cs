namespace XUnitCompleteExample.Cartellino;

[Collection("TestCollectionFixture")]
public class TemporaryTest : ControllerTestBase, IClassFixture<TemporaryControllersFixture>
{
    protected readonly TemporaryControllersFixture _fixture;
    private readonly TestFixture _testFixture;

    public TemporaryTest(
        TestFixture testFixture,
        TemporaryControllersFixture fixture,
        ITestOutputHelper testOutputHelper
        ) : base(testFixture, testOutputHelper)
    {
        _fixture = fixture;
        _testFixture = testFixture;
    }
    
    //In this class put all tests that are temporary. For example put here a simple HTTP request to test quickly an API.
    [Fact]
    public async Task Test()
    {
        //Do something.
    }
}



