namespace XUnitCompleteExample.Identity.Repository;

public class UserRepository : IUserRepository
{
    protected readonly XUnitCompleteExampleIdentityDbContext _XUnitCompleteExampleIdentityDbContext;
    protected readonly ILogService _logService;
    private readonly Texts _texts = Texts.GetInstance();

    public UserRepository(XUnitCompleteExampleIdentityDbContext xUnitCompleteExampleIdentityDbContext, ILogService logService)
    {
        _XUnitCompleteExampleIdentityDbContext = xUnitCompleteExampleIdentityDbContext;
        _logService = logService;
    }

    public bool Add(User user)
    {
        var context = new Dictionary<string, object>
        {
            {_texts.MethodName, nameof(Add)},
            {_texts.ClassName, nameof(UserRepository)},
            {nameof(user.Username), user.Username}
        };
        try
        {
            _XUnitCompleteExampleIdentityDbContext.Add(user);
            return Salva();
        }
        catch (Exception ex)
        {
            _logService.LogError(ex: ex, context: context);
            throw;
        }
    }
    
    public bool Delete(long id)
    {
        var context = new Dictionary<string, object>
        {
            {_texts.MethodName, nameof(Add)},
            {_texts.ClassName, nameof(UserRepository)},
            {nameof(id), id}
        };
        try
        {
            User user = _XUnitCompleteExampleIdentityDbContext.Users.FirstOrDefault(user => user.Id == id);
            _XUnitCompleteExampleIdentityDbContext.Remove(user);
            return Salva();
        }
        catch (Exception ex)
        {
            _logService.LogError(ex: ex, context: context);
            throw;
        }
    }

    public async Task<List<User>> GetAll()
    {
        return await _XUnitCompleteExampleIdentityDbContext.Set<User>().ToListAsync();
    }
    
    // public async Task<User> GetById(Guid guidUtente)
    // {
    //     try
    //     {
    //         return await _XUnitCompleteExampleIdentityDbContext.Set<User>()
    //                                .Where(x => x.GuidUtente.Equals(guidUtente))
    //                                .FirstOrDefaultAsync();
    //     }
    //     catch (Exception exception)
    //     {
    //         _logService.LogError(exception, $"An error occurred while getting utente with GuidUtente: {guidUtente}.");
    //         throw;
    //     }
    // }
    // public User GetUtenteNoTrack(Guid guidUtente)
    // {
    //     try
    //     {
    //         return _XUnitCompleteExampleIdentityDbContext
    //             .Set<User>()
    //             .AsNoTracking()
    //             .SingleOrDefault(x => x.GuidUtente.Equals(guidUtente));
    //     }
    //     catch (Exception exception)
    //     {
    //         _logService.LogError(exception, $"An error occurred while getting utente with GuidUtente: {guidUtente} without tracking it.");
    //         throw;
    //     }
    //
    // }
    //
    // public bool Exists(Guid guidUtente)
    // {
    //     return _XUnitCompleteExampleIdentityDbContext.Utente
    //         .Any(utente => utente.GuidUtente == guidUtente);
    // }
    //
    public bool Salva()
    {
        var saved = _XUnitCompleteExampleIdentityDbContext.SaveChanges();
        return saved >= 0;
    }
    
    // Esempio di transazione.
    // // Questa funzione aggiorna tutti i campi di TB_UTENTI tranne Password, PasswordH e Salt.
    // public bool Update(User updatedUser)
    // {
    //     bool savingResult;
    //     using IDbContextTransaction transaction = _XUnitCompleteExampleIdentityDbContext.Database.BeginTransaction();
    //     try
    //     {
    //         User notUpdatedUser = GetUtenteNoTrack(updatedUser.GuidUtente);
    //         // Controlliamo se il record Utente e il record TB_Username attuali sono allineati.
    //         string userName = _XUnitCompleteExampleIdentityDbContext.TB_Username.FirstOrDefault(userName => userName.Username == notUpdatedUser.Username).Username;
    //         if (userName == null)
    //             return false;
    //         // Gestiamo il caso in cui tra le propriet� aggiornate c'� Username.
    //         if (userName != updatedUser.Username)
    //         {
    //             // Aggiungiamo un record TB_Username (il vecchio Username non sarà più associato ad alcun record Utente).
    //             _XUnitCompleteExampleIdentityDbContext.TB_Username.Add(new TB_Username()
    //             {
    //                 Username = updatedUser.Username,
    //                 DataPrimoUtilizzo = DateTime.UtcNow
    //             });
    //             savingResult = Salva();
    //             updatedUser.PasswordH = notUpdatedUser.PasswordH;
    //             updatedUser.Salt = notUpdatedUser.Salt;
    //             updatedUser.Password = notUpdatedUser.Password;
    //             _XUnitCompleteExampleIdentityDbContext.Update(updatedUser);
    //             savingResult = Salva();
    //             transaction.Commit();
    //             return savingResult;
    //         }
    //         _XUnitCompleteExampleIdentityDbContext.Update(updatedUser); // Gestiamo il caso in cui tra le proprietà aggiornato non c'è Username.
    //         savingResult = Salva();
    //         return savingResult;
    //     }
    //     catch (Exception exception)
    //     {
    //         transaction.Rollback();
    //         _logService.LogError($"An error occurred while updating utente {updatedUser.GuidUtente}.");
    //         throw;
    //     }
    // }
    
    public bool Update(User updatedUser)
    {
        var context = new Dictionary<string, object>
        {
            {_texts.MethodName, nameof(Update)},
            {_texts.ClassName, nameof(UserRepository)},
            {nameof(updatedUser), updatedUser}
        };
        
        try
        {
            User notUpdatedUser = GetUserNoTrack(updatedUser.Id);
            updatedUser.PasswordHash = notUpdatedUser.PasswordHash;
            updatedUser.Salt = notUpdatedUser.Salt;
            _XUnitCompleteExampleIdentityDbContext.Update(updatedUser);
            return Salva();
        }
        catch (Exception ex)
        {
            var error = new Error(_texts.LogGenericError, context, ex);
            _logService.LogError(error);
            throw;
        }
    }
    
    // public bool UpdatePassword(byte[] passwordH, byte[] salt, string username)
    // {
    //     try
    //     {
    //         User user = GetUserByUsername(username).Result;
    //         if (user == null || passwordH == null || salt == null)
    //             return false;
    //         user.PasswordH = passwordH;
    //         user.Salt = salt;
    //         return Salva();
    //     }
    //     catch (Exception exception)
    //     {
    //         // DA FARE.
    //         // Valutare se usare o meno un messaggio esplicito.
    //         _logService.LogError(exception, "An error occurred while updating password.");
    //         throw;
    //     }
    // }
    
    public User GetUserNoTrack(long userId)
    {
        
        var context = new Dictionary<string, object>
        {
            {_texts.MethodName, nameof(GetUserNoTrack)},
            {_texts.ClassName, nameof(UserRepository)},
            {nameof(userId), userId}
        };
        try
        {
            return _XUnitCompleteExampleIdentityDbContext.
                Users
                .AsNoTracking()
                .SingleOrDefault(user => user.Id == userId);
        }
        catch (Exception ex)
        {
            var error = new Error(_texts.LogGenericError, context, ex);
            _logService.LogError(error);
            throw;
        }

    }
    
    public async Task<User> GetById(long id)
    {
        return await _XUnitCompleteExampleIdentityDbContext.Users.FindAsync(id);
    }
    public async Task<User> GetByUsername(string username)
    {
        return await _XUnitCompleteExampleIdentityDbContext.Users.Where(x => x.Username == username).SingleOrDefaultAsync();
    }
}