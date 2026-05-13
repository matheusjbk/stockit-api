using StockIt.Domain.Entities;

namespace StockIt.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    public string GenerateToken(User user);
}
