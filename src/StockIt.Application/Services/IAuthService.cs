using StockIt.Application.Security;
using StockIt.Domain.Entities;
using StockIt.Domain.Shared;

namespace StockIt.Application.Services;

public interface IAuthService
{
    public Task<Result> CreateUserAsync(User user, string password);
    public Task<Result<AuthenticatedUser>> LoginAsync(string email, string password);
    public Task<Result> AddToRoleAsync(User user, string role);
}
