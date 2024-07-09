namespace XUnitCompleteExample.Identity.Extensions;

public static class ApplicationOptionsExtensions
{
    public static ApiBehaviorOptions FormatInvalidModelStateResponse(this ApiBehaviorOptions options)
    {
        // Uniformiamo, impostando una formattazione, le risposte date dal framework in caso di model state invalido.
        options.InvalidModelStateResponseFactory = context =>
        {
            var errorsDtos = new List<ErrorDto>();

            foreach (string item in context.ModelState.Keys)
            {
                List<ModelError> modelErrors = context.ModelState[item].Errors.ToList();
                foreach (ModelError modelError in modelErrors)
                {
                    errorsDtos.Add(new ErrorDto(modelError.ErrorMessage));
                }
            }

            return new JsonResponse<List<ErrorDto>>(StatusCodes.Status422UnprocessableEntity, errorsDtos);
        };
        return options;
    }
}