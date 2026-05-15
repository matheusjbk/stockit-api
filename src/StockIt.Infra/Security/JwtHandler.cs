using Microsoft.IdentityModel.Tokens;
using StockIt.Domain.Security;
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

    protected static ClaimsIdentity GenerateClaims(AuthenticatedUser user)
    {
        var claims = new ClaimsIdentity();

        claims.AddClaims(
            [
                new(ClaimTypes.Sid, user.Email),
                new(ClaimTypes.GroupSid, user.CompanyId.ToString()),
                new(ClaimTypes.Role, user.Role)
            ]);

        return claims;
    }
}
