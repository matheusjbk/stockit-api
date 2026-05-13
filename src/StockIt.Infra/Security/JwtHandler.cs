using Microsoft.IdentityModel.Tokens;
using StockIt.Domain.Entities;
using System.Security.Claims;
using System.Text;

namespace StockIt.Infra.Security;

public abstract class JwtHandler
{
    protected static SymmetricSecurityKey GenerateSecurityKey(string secretKey)
    {
        var bytes = Encoding.UTF8.GetBytes(secretKey);

        return new SymmetricSecurityKey(bytes);
    }

    protected static ClaimsIdentity GenerateClaims(User user)
    {
        var claims = new ClaimsIdentity();

        claims.AddClaim(new(ClaimTypes.Sid, user.Email));

        return claims;
    }
}
