using Microsoft.AspNetCore.Mvc;

namespace XUnitCompleteExample.Models
{
    public class JsonResponse<T> : ActionResult
    {
        protected readonly JsonSerializerSettings _jsonSettings = new() { Converters = { new StringEnumConverter() } };
        public int StatusCode { get; set; }
        public T Body { get; set; }
        public JsonResponse(int statusCode, T body)
        {
            StatusCode = statusCode;
            Body = body;
        }
        // Generiamo il json da inserire nella risposta.
        public override void ExecuteResult(ActionContext context)
        {
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = StatusCode;
            response.ContentType = MediaTypeNames.Application.Json;
            string bodyJson = JsonConvert.SerializeObject(Body);
            response.WriteAsync(bodyJson);
        }
    }
}
