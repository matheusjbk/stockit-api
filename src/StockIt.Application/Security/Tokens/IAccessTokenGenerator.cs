namespace StockIt.Application.Security.Tokens;

public interface IAccessTokenGenerator
{
    public string GenerateToken(AuthenticatedUser user);
}
