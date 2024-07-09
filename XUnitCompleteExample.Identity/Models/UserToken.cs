namespace XUnitCompleteExample.Identity.Models
{
    public class UserToken
    {
        public User User { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; }

    }
}
