namespace XUnitCompleteExample.Identity.Configurations
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public int Expiration { get; set; }
    }
}
