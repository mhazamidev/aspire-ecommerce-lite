using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineShop.Shared.Identity;

public static class Helpers
{
    public static List<Claim> ExtractJWTClaims(this string jwtToken)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var decodeToken = jwtTokenHandler.ReadJwtToken(jwtToken);
        return decodeToken.Claims.ToList();
    }
}
