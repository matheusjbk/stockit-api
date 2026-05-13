using StockIt.Domain.Entities;
using StockIt.Domain.Shared;

namespace StockIt.Domain.Services;

public interface IAuthService
{
    public Task<Result> CreateUserAsync(User user, string password);
    public Task<Result> LoginAsync(string email, string password);
}
