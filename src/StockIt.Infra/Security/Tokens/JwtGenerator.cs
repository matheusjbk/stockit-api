using Microsoft.IdentityModel.Tokens;
using StockIt.Application.Security;
using StockIt.Application.Security.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace StockIt.Infra.Security.Tokens;

public class JwtGenerator(uint expirationTimeInMinutes, string secretKey) : JwtHandler, IAccessTokenGenerator
{
    private readonly uint _expirationTimeInMinutes = expirationTimeInMinutes;
    private readonly string _secretKey = secretKey;

    public string GenerateToken(AuthenticatedUser user)
    {
        var credentials = new SigningCredentials(GenerateSecurityKey(_secretKey), SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeInMinutes),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
