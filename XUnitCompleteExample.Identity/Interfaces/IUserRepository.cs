namespace XUnitCompleteExample.Identity.Interfaces;

public interface IUserRepository
{
    bool Add(User user);
    bool Delete(long id);
    Task<List<User>> GetAll();
    // Task<User> GetById(Guid guidUtente);
    // User GetUtenteNoTrack(Guid guidUtente);
    // bool Exists(Guid guidUtente);
    // bool Salva();
    bool Update(User updatedUser);
    // bool UpdatePassword(byte[] passwordH, byte[] salt, string username);
    Task<User> GetById(long userId);
    Task<User> GetByUsername(string username);
}