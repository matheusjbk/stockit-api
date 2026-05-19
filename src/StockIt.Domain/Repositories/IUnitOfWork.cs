namespace StockIt.Domain.Repositories;

public interface IUnitOfWork
{
    public IUserRepository Users { get; }
    public ICompanyRepository Companies { get; }
    public ICategoryRepository Categories { get; }

    public Task BeginTransactionAsync();
    public Task CommitTransactionAsync();
    public Task SaveAsync();
    public Task RollbackAsync();
}
