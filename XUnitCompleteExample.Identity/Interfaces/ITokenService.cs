namespace XUnitCompleteExample.Identity.Interfaces;

public interface ITokenService
{
    SecureToken GenerateJwtToken(long userId);
}