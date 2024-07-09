namespace XUnitCompleteExample.Identity.Models
{
    public class SecureToken
    {
        public string Token { get; set; }
        //[Newtonsoft.Json.JsonProperty("dateTimeField", Required = Newtonsoft.Json.Required.Always)]
        public DateTime Expiration { get; set; }
    }
}
