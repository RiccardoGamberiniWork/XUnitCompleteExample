namespace XUnitCompleteExample.Identity.Services
{
    internal class TokenService : ITokenService
    {
        private const string secret = "XUnitCompleteExample-Identity1qa2ws3ed4rf5tg6yh7ujki8SuperSecretKey@lo98ikju76yh";
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config) => _config = config;

        SecureToken ITokenService.GenerateJwtToken(long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(GetSecret());  
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("userId", userId.ToString()) }),
                //Expires = DateTime.UtcNow.AddDays(1),
                Expires = DateTime.UtcNow.AddHours(GetExpiration()),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);
            return new SecureToken { Token = new JwtSecurityTokenHandler().WriteToken(token), Expiration = token.ValidTo };
        }

        private string GetJWTKey => _config["JWTAuth:Secret"];
        private string GetExpired => _config["JWTAuth:Expired"];

        private int GetExpiration()
        {
            var valueDefault = 3;

            var exp = GetExpired;
            if (int.TryParse(exp, out int result))
                return result;
            return valueDefault;
        }

        private string GetSecret()
        {
            if (!string.IsNullOrWhiteSpace(GetJWTKey))
                return GetJWTKey;
            return secret;
        }
    }
}