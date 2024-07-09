#nullable enable

namespace XUnitCompleteExample.Identity.Services;

public class JsonResponseFormatService : IJsonResponseFormatService
{
    private readonly IMapper _mapper;

    public JsonResponseFormatService(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    #region Funzioni per risposte HTTP con status compreso nell'intevallo 200-299.
    JsonResponse<string> IJsonResponseFormatService.Success()
    {
        return new JsonResponse<string>(StatusCodes.Status200OK, string.Empty);
    }

    JsonResponse<T> IJsonResponseFormatService.Success<T>(T obj, int statusCode)
    {
        return new JsonResponse<T>(statusCode, obj);
    }
    #endregion
    #region Funzioni per risposte HTTP con status compreso nell'intevallo 400-599.
    JsonResponse<List<ErrorDto>> IJsonResponseFormatService.Error(string msg, int statusCode)
    {
        var errorDto = new ErrorDto(msg);
        return new JsonResponse<List<ErrorDto>>(statusCode, new List<ErrorDto> { errorDto });
    }
    
    JsonResponse<List<ErrorDto>> IJsonResponseFormatService.Error(Error error, int statusCode)
    {
        return new JsonResponse<List<ErrorDto>>(statusCode, _mapper.Map<List<ErrorDto>>(new List<Error> { error }));
    }
    
    JsonResponse<List<ErrorDto>> IJsonResponseFormatService.Error(Information information, int statusCode)
    {
        var errorDto = _mapper.Map<ErrorDto>(information);
        return new JsonResponse<List<ErrorDto>>(statusCode, new List<ErrorDto> {errorDto});
    }
    
    JsonResponse<List<ErrorDto>> IJsonResponseFormatService.Error(List<Information> informations, int statusCode)
    {
        return new JsonResponse<List<ErrorDto>>(statusCode, _mapper.Map<List<ErrorDto>>(informations));
    }
    
    JsonResponse<List<ErrorDto>> IJsonResponseFormatService.Error(ValidationResult validationResult, int statusCode)
    {
        var errorsDtos = new List<ErrorDto>();
        validationResult.Errors.ForEach(validationFailure => errorsDtos.Add(new ErrorDto(validationFailure.ErrorMessage, DateTime.Now)));
        return new JsonResponse<List<ErrorDto>>(statusCode, errorsDtos);
    }
    #endregion
}