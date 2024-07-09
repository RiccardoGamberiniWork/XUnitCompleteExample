namespace XUnitCompleteExample.Models
{
    public class ApiResponseBody
    {
        // DA FARE.
        // Evitare di comunicare al client la versione delle api usando un dato hard coded.
        public string ApiVersion { get; set; } = "1.0.0.0";
    }
}
