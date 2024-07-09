namespace XUnitCompleteExample.Identity.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> Authenticate(string Username, string Password);
        UserContext UserContext { get; }
    }
}
