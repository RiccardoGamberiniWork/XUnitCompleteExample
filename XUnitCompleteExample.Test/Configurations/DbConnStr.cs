namespace XUnitCompleteExample.Configurations;

public class DbConnStr
{
    public string DataSource {get; set;}
    public string UserID {get; set;}
    public string Password {get; set;}
    public string InitialCatalog {get; set;}
    public bool TrustedConnection {get; set;}
    public bool MultipleActiveResultSets {get; set;}
    public bool Encrypt {get; set;}
    public bool TrustServerCertificate {get; set;}
}