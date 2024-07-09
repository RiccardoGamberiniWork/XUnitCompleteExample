namespace XUnitCompleteExample.Identity.Repository;

public class AuthRepository: IAuthRepository
{
    protected readonly IPasswordHasher _passwordHasher;
    private UserContext _userContext;
    private readonly IMapper _mapper;
    protected readonly IUserRepository _userRepository;
    protected readonly ILogger<AuthRepository> _logger;
    protected readonly AppSettingsConfig _appSettingsConfig;

    public AuthRepository(
        IPasswordHasher passwordHasher,
        IUserRepository userRepository,
        ILogger<AuthRepository> logger,
        IOptions<AppSettingsConfig> appSettingsConfig,
        IMapper mapper
        )
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _logger = logger;
        _appSettingsConfig = appSettingsConfig.Value;
        _mapper = mapper;
    }

    public UserContext UserContext =>_userContext;

    public async Task<bool> Authenticate(string username, string password)
    {
        User user = await _userRepository.GetByUsername(username);
        var loggerUserDto = _mapper.Map<LoggedUserDto>(user);
        HashWithSaltResult hashResult = _passwordHasher.HashWithSalt(password, user.Salt, _passwordHasher.HashAlgorithm);
        if (_passwordHasher.HashCompare(user.PasswordHash, hashResult.Digest))
        {
            _userContext = new UserContext { LoggedUser = loggerUserDto };
            return true;
        }

        return false;
    }
}