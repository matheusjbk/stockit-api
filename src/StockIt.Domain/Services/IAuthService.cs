using StockIt.Domain.Entities;

namespace StockIt.Domain.Services;

public interface IAuthService
{
    public Task<bool> CreateUserAsync(User user, string password);
}
