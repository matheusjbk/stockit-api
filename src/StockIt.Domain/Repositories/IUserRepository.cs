using StockIt.Domain.Entities;

namespace StockIt.Domain.Repositories;

public interface IUserRepository
{
    public Task<bool> ExistUserWithEmail(string email);

    public Task<User?> GetUserByEmail(string email);
}
