using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Com.Garment.Shipping.ETL.Service.Services
{
    public class TokenPayloadExtractorService
    {
        JwtSecurityToken decodedToken;
        public TokenPayloadExtractorService(string token) {
            var handler = new JwtSecurityTokenHandler();
            this.decodedToken = handler.ReadJwtToken(token) as JwtSecurityToken;
        }

        public string GetUsername() {
            var username = decodedToken.Claims.First(claim => claim.Type == "username").Value;
            return username;
        }
    }
}