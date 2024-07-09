namespace XUnitCompleteExample.Fixtures.User.UserController;

public class UserControllerFixture : IDisposable
{
    private readonly Credentials _mockedTestUserCredentials = TestBase.MockJsonFile<Credentials>("TestUserCredentials");
    
    public Credentials MockedTestUserCredentials => _mockedTestUserCredentials;
    public void Dispose()
    {
        // Dispose of any resources here
    }
}