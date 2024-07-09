namespace XUnitCompleteExample.Identity.Controllers;

[Consumes("application/json")]
[Produces("application/json")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class AuthController : ControllerBase
{
    protected readonly ILogger<AuthController> _logger;
    protected readonly JsonSerializerSettings _jsonSettings = null;
    protected readonly ITokenService _tokenService;
    protected readonly IAuthRepository _authRepository;
    private readonly IJsonResponseFormatService _jsonResponseFormatService;

    public AuthController(ILogger<AuthController> logger,
        ITokenService tokenService,
        IAuthRepository authRepository,
        IJsonResponseFormatService jsonResponseFormatService)
    {
        _logger = logger;
        _jsonSettings = new JsonSerializerSettings {Converters={new StringEnumConverter()}};
        _tokenService = tokenService;
        _authRepository = authRepository;
        _jsonResponseFormatService = jsonResponseFormatService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [AllowAnonymous]
    public async Task<ActionResult<UserContext>> Authenticate([FromBody] Credentials credentials)
    {
        _logger.LogInformation($"Login for username: {credentials.Username} ");
        if (string.IsNullOrWhiteSpace(credentials.Username) || credentials.Username == string.Empty || string.IsNullOrWhiteSpace(credentials.Password) || credentials.Password == string.Empty)
            return _jsonResponseFormatService.Error(msg: "Username or password isn't valid.");

        UserContext userContext;
        bool loginResult = await _authRepository.Authenticate(credentials.Username, credentials.Password);
        if (loginResult){
            userContext = _authRepository.UserContext;
            userContext.SecureToken = _tokenService.GenerateJwtToken(userContext.LoggedUser.Id);
            return _jsonResponseFormatService.Success(obj: userContext);
        }

        return _jsonResponseFormatService.Error(msg: "Unauthorized access.", StatusCodes.Status401Unauthorized);
    }
}