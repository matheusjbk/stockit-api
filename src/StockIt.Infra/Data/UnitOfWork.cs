using Microsoft.EntityFrameworkCore.Storage;
using StockIt.Domain.Repositories;
using StockIt.Infra.Data.Repositories;

namespace StockIt.Infra.Data;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IUserRepository? _userRepository;
    private ICompanyRepository? _companyRepository;
    private ICategoryRepository? _categoryRepository;
    private IDbContextTransaction? _transaction;

    public IUserRepository Users => _userRepository ??= new UserRepository(context);

    public ICompanyRepository Companies => _companyRepository ??= new CompanyRepository(context);

    public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(context);

    public async Task BeginTransactionAsync()
    {
        _transaction ??= await context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if(_transaction is not null)
        {
            await context.SaveChangesAsync();

            await _transaction.CommitAsync();

            await _transaction.DisposeAsync();

            _transaction = null;
        }
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        if (_transaction is not null)
        {

            await _transaction.RollbackAsync();

            await _transaction.DisposeAsync();

            _transaction = null;
        }
    }
}
