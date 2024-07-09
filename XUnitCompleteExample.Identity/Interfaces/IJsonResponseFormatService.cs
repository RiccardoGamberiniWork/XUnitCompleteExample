namespace XUnitCompleteExample.Identity.Interfaces
{
    public interface IJsonResponseFormatService
    {
        JsonResponse<string> Success();
        JsonResponse<T> Success<T>(T obj, int statusCode = StatusCodes.Status200OK);
        JsonResponse<List<ErrorDto>> Error(string msg, int statusCode = StatusCodes.Status422UnprocessableEntity);
        JsonResponse<List<ErrorDto>> Error(Error error, int statusCode);
        JsonResponse<List<ErrorDto>> Error(Information information, int statusCode = StatusCodes.Status422UnprocessableEntity);
        JsonResponse<List<ErrorDto>> Error(List<Information> informations, int statusCode);
        JsonResponse<List<ErrorDto>> Error(ValidationResult validationResult, int statusCode);
    }
}