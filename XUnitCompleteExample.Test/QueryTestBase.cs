using XUnitCompleteExample.Fixtures;

namespace XUnitCompleteExample;

public class QueryTestBase : TestBase
{
    protected readonly TestFixture _testFixture;

    public QueryTestBase(TestFixture testFixture, ITestOutputHelper testOutputHelper) : base (testOutputHelper)
    {
        _testFixture = testFixture;
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
    
    public async Task CloseConnectionAndFailTest(string msg = "TEST FAILED", [CallerLineNumber] int lineNumber = 0)
    {
        await _testFixture.SqlConnection.CloseAsync();
        _testOutputHelper.WriteLine($"{msg} =====> {lineNumber}");
        Assert.True(false);
    }
}