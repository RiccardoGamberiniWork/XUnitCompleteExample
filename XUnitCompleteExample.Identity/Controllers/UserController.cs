namespace XUnitCompleteExample.Identity.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly ILogService _logService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IJsonResponseFormatService _jsonResponseFormatService;
    private readonly IValidator<long> _idValidator;
    private readonly IValidator<AddUserDto> _addUserDtoValidator;
    private readonly IValidator<UpdateUserDto> _updateUserDtoValidator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly Texts _texts = Texts.GetInstance();
    
    public UserController(ILogService logService,
        IUserRepository userRepository,
        IMapper mapper,
        IJsonResponseFormatService jsonResponseFormatService,
        IValidator<long> idValidator,
        IValidator<AddUserDto> addUserDtoValidator,
        IValidator<UpdateUserDto> updateUserDtoValidator,
        IPasswordHasher passwordHasher)
    {
        _logService = logService;
        _userRepository = userRepository;
        _mapper = mapper;
        _jsonResponseFormatService = jsonResponseFormatService;
        _idValidator = idValidator;
        _addUserDtoValidator = addUserDtoValidator;
        _updateUserDtoValidator = updateUserDtoValidator;
        _passwordHasher = passwordHasher;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessBody<List<UserDto>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(List<ErrorDto>))]
    public async Task<IActionResult> GetAll()
    {
        var context = new Dictionary<string, object>
        {
            {_texts.MethodName, nameof(Get)},
            {_texts.ClassName, nameof(UserController)}
        };
        var information = new Information { Context = context };
        _logService.LogInformation(msg: _texts.LogInformationEndpointCall, context: context);
        List<User> usersList;
        
        try
        {
            usersList = await _userRepository.GetAll();
            if (null == usersList)
            {
                var informationMsg = string.Format(_texts.AnyFound, _texts.UserLowerCase);
                information.UserInformationMsg = informationMsg;
                information.LogInformationMsg = informationMsg;
                _logService.LogInformation(information);
                return _jsonResponseFormatService.Error(msg: "Any user found.", statusCode: StatusCodes.Status404NotFound);
            }
        }
        catch (Exception ex)
        {
            _logService.LogError(ex: ex, context: context);
            return _jsonResponseFormatService.Error(msg: _texts.UserGenericError, statusCode: StatusCodes.Status500InternalServerError);
        }
    
        List<UserDto> userDtoList = _mapper.Map<List<UserDto>>(usersList);
        return _jsonResponseFormatService.Success(obj: userDtoList);
    }
    
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessBody<UserDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(List<ErrorDto>))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(List<ErrorDto>))]
    public async Task<IActionResult> Get([FromQuery] long id)
    {
        var context = new Dictionary<string, object>
        {
            {_texts.MethodName, nameof(Get)},
            {_texts.ClassName, nameof(UserController)},
            {nameof(id), id}
        };
        var information = new Information { Context = context };
        _logService.LogInformation(msg: _texts.LogInformationEndpointCall, context: context);
        ValidationResult validationResult = _idValidator.Validate(id);
        
        if (!validationResult.IsValid)
        {
            information.UserInformationMsg = string.Format(_texts.MustNotEqualTo, nameof(id), id.ToString());
            return _jsonResponseFormatService.Error(information: information, statusCode: StatusCodes.Status422UnprocessableEntity);
        }
        
        User user;
        
        try
        {
            user = await _userRepository.GetById(id);
            if (user == null)
            {
                var informationMsg = string.Format(_texts.AnyFound, _texts.UserLowerCase);
                information.UserInformationMsg = informationMsg;
                information.LogInformationMsg = informationMsg;
                _logService.LogInformation(information);
                return _jsonResponseFormatService.Error(information: information, statusCode: StatusCodes.Status404NotFound);
            }
        }
        catch (Exception ex)
        {
            _logService.LogError(ex: ex, context: context);
            return _jsonResponseFormatService.Error(msg: _texts.UserGenericError, statusCode: StatusCodes.Status500InternalServerError);
        }

        UserDto userDto = _mapper.Map<UserDto>(user);
        return _jsonResponseFormatService.Success(obj: userDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SuccessBody<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ErrorDto>))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(List<ErrorDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ErrorDto>))]
    // Alla funzione AddUser è stato passato AddUserDto perchè l'entità User ha proprietà che non serve passare in fase di creazione dell'user.
    public IActionResult Add([FromBody] AddUserDto addUserDto)
    {
        var information = new Information();
        
        if (addUserDto == null)
        {
            information.LogInformationMsg = _texts.RequestBodyNotNull;
            information.UserInformationMsg = _texts.RequestBodyNotNull;
            var response = _jsonResponseFormatService.Error(information: information, statusCode: StatusCodes.Status400BadRequest);
            return response;
        }
        
        var context = new Dictionary<string, object>
        {
            {_texts.MethodName, nameof(Add)},
            {_texts.ClassName, nameof(UserController)},
            {nameof(addUserDto.Username), addUserDto.Username}
        };
        information.Context = context;
        var error = new Error { Context = context };
        _logService.LogInformation(msg: _texts.LogInformationEndpointCall, context: context);
        ValidationResult validationResult = _addUserDtoValidator.Validate(addUserDto);

        if (!validationResult.IsValid)
        {
            JsonResponse<List<ErrorDto>> response = _jsonResponseFormatService.Error(validationResult: validationResult, statusCode: StatusCodes.Status422UnprocessableEntity);
            return response;
        }

        var user = _mapper.Map<User>(addUserDto);
        // Proteggiamo la password.
        byte[] salt = _passwordHasher.CreateSalt();
        user.Salt = salt;
        HashWithSaltResult hashResult = _passwordHasher.HashWithSalt(addUserDto.Password, salt, _passwordHasher.HashAlgorithm);
        user.PasswordHash = hashResult.Digest;
        string errorMsg = $"{_texts.AnErrorOccurredWhile} adding {nameof(User).ToLower()}.";
        
        try
        {
            
            if (!_userRepository.Add(user))
            {
                error.LogErrorMsg = errorMsg;
                JsonResponse<List<ErrorDto>> response = _jsonResponseFormatService.Error(error: error, statusCode: StatusCodes.Status500InternalServerError);
                return response;
            }
            
        }
        catch (Exception ex)
        {
            _logService.LogError(msg: errorMsg, ex: ex, context: context);
            return _jsonResponseFormatService.Error(error: error, statusCode: StatusCodes.Status500InternalServerError);
        }
        
        return _jsonResponseFormatService.Success(obj: $"User {user.Username} added successfully.", statusCode: StatusCodes.Status201Created);
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessBody<string>))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(List<ErrorDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ErrorDto>))]
    public IActionResult Delete(long id)
    {
        var context = new Dictionary<string, object>
        {
            {_texts.MethodName, nameof(Delete)},
            {_texts.ClassName, nameof(UserController)},
            {nameof(id), id}
        };
        var information = new Information { Context = context };
        var error = new Error { Context = context };
        _logService.LogInformation(msg: _texts.LogInformationEndpointCall, context: context);
        ValidationResult validationResult = _idValidator.Validate(id);
        
        if (!validationResult.IsValid)
        {
            information.UserInformationMsg = string.Format(_texts.MustNotEqualTo, nameof(id), id.ToString());
            return _jsonResponseFormatService.Error(information: information, statusCode: StatusCodes.Status422UnprocessableEntity);
        }
        
        string errorMsg = $"{_texts.AnErrorOccurredWhile} deleting {nameof(User).ToLower()}.";
        
        try
        {
            if (!_userRepository.Delete(id))
            {
                error.LogErrorMsg = errorMsg;
                return _jsonResponseFormatService.Error(error: error, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
        catch (Exception ex)
        {
            _logService.LogError(msg: errorMsg, ex: ex, context: context);
            return _jsonResponseFormatService.Error(error: error, statusCode: StatusCodes.Status500InternalServerError);
        }
    
        return _jsonResponseFormatService.Success(obj: $"User {id} deleted successfully.");
    }
    
    // [HttpPut("update")]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessBody<string>))]
    // [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ErrorDto>))]
    // [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(List<ErrorDto>))]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ErrorDto>))]
    // // Alla funzione AddUser è stato passato UpdateUserDto perchè l'entità User ha proprietà che non serve passare in fase di creazione dell'user.
    // public IActionResult Update([FromBody] UpdateUserDto updateUserDto)
    // {
    //     var context = new Dictionary<string, object>
    //     {
    //         {_texts.MethodName, nameof(Update)},
    //         {_texts.ClassName, nameof(UserController)},
    //         {nameof(updateUserDto), updateUserDto}
    //     };
    //     
    //     _logService.LogInformation(msg: _texts.LogInformationEndpointCall);
    //     if (updateUserDto == null)
    //         return _jsonResponseFormatService.Error(msg: "User can't be null.", statusCode: StatusCodes.Status400BadRequest);
    //     
    //     if (!_updateUserDtoValidator.Validate(updateUserDto).IsValid)
    //         return _jsonResponseFormatService.Error(msg: "User is invalid.", statusCode: StatusCodes.Status422UnprocessableEntity);
    //     
    //     var user = _mapper.Map<User>(updateUserDto);
    //     string errorMessage = $"An error occurred while updating user {user.Id}.";
    //     var error = new Error(errorMessage, context);
    //
    //     try
    //     {
    //         if (!_userRepository.Update(user))
    //         {
    //             _logService.LogError(error);
    //             return _jsonResponseFormatService.Error(msg: errorMessage, statusCode: StatusCodes.Status500InternalServerError);
    //         }
    //         _logService.LogInformation(msg: $"User {user.Id} updated successfully.");
    //     }
    //     catch (Exception ex)
    //     {
    //         error.Ex = ex;
    //         _logService.LogError(error);
    //         return _jsonResponseFormatService.Error(msg: errorMessage, statusCode: StatusCodes.Status500InternalServerError);
    //     }
    //
    //     return _jsonResponseFormatService.Success(obj: $"User {user.Id} updated successfully.");
    // }
    
    // [HttpPut("updatePassword")]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessBody<string>))]
    // [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ErrorDto>))]
    // [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(List<ErrorDto>))]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<ErrorDto>))]
    // public IActionResult UpdatePassword([FromBody] UpdatePasswordDto updatePasswordDto)
    // {
    //     _logService.LogInformation($"UpdatePassword.");
    //     if (updatePasswordDto == null)
    //         // Non formattiamo ModelState perchè in ogni caso non forniamo nessuna informazione nella risposta.
    //         return _jsonResponseFormatService.Error(statusCode: StatusCodes.Status400BadRequest);// Evitiamo di fornire infomazioni utili ad un attaccante.
    //     
    //     if (!_updatePasswordDtoValidator.Validate(updatePasswordDto).IsValid)
    //         return _jsonResponseFormatService.Error(statusCode: StatusCodes.Status400BadRequest);// Evitiamo di fornire infomazioni utili ad un attaccante.
    //
    //     // Verifichiamo che la nuova password sia diversa alla precedente.
    //     bool isPreviousPassword;
    //     try
    //     {
    //         // NOTA. Eventualmente aggiungere il seguente controllo:
    //         // se la password nuova è  uguale a una di quelle usate dall’user interessato dalla modifica.
    //         // Siccome attualmente non esiste uno storico delle password di ogni user la ValidatePasswordUsage
    //         // esegue il controllo solo sull'ultima password salvata.
    //         User user = _userRepository.GetUserByUsername(updatePasswordDto.Username).Result;
    //         HashWithSaltResult hashResult = _passwordHasher.HashWithSalt(updatePasswordDto.Password, user.Salt, _passwordHasher.HashAlgorithm);
    //         isPreviousPassword = _passwordHasher.HashCompare(hashResult.Digest, user.PasswordH);
    //         //
    //     }
    //     catch (Exception)
    //     {
    //         return _jsonResponseFormatService.Error();// Evitiamo di fornire infomazioni utili ad un attaccante.
    //     }
    //
    //     if (isPreviousPassword)
    //         return _jsonResponseFormatService.Error();// Evitiamo di fornire infomazioni utili ad un attaccante.
    //
    //     // Aggiorniamo la password.
    //     byte[] newSalt = _passwordHasher.CreateSalt();
    //     byte[] newPasswordH = _passwordHasher.HashWithSalt(updatePasswordDto.Password, newSalt, _passwordHasher.HashAlgorithm).Digest;
    //     string errorMessage = $"An error occurred while updating user password.";
    //     try
    //     {
    //         if (!_userRepository.UpdatePassword(newPasswordH, newSalt, updatePasswordDto.Username))
    //             return _jsonResponseFormatService.Error();// Evitiamo di fornire infomazioni utili ad un attaccante.
    //     }
    //     catch (Exception)
    //     {
    //         _logService.LogError(errorMessage);
    //         return _jsonResponseFormatService.Error();// Evitiamo di fornire infomazioni utili ad un attaccante.
    //     }
    //
    //     return _jsonResponseFormatService.Success();// Evitiamo di fornire infomazioni utili ad un attaccante.
    // }
}
