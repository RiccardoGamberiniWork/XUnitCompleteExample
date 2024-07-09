namespace XUnitCompleteExample.Identity.Models;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] Salt { get; set; }
}