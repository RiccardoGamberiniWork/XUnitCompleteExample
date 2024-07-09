namespace XUnitCompleteExample.Identity.Models;

public class UserContext
{
    public LoggedUserDto LoggedUser;
    public SecureToken SecureToken {get; set;}
}