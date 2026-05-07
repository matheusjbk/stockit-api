using StockIt.Domain.Entities;

namespace StockIt.Domain.Services;

public interface IAuthService
{
    public Task<bool> CreateUserAsync(string name, string email, string password);
}
