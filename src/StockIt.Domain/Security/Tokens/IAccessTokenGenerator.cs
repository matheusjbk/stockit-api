namespace StockIt.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    public string GenerateToken(AuthenticatedUser user);
}
