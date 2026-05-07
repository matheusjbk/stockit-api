namespace StockIt.Domain.Repositories;

public interface IUserRepository
{
    public Task<bool> ExistUserWithEmail(string email);
}
