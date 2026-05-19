using StockIt.Domain.Entities;

namespace StockIt.Domain.Repositories;

public interface ICompanyRepository
{
    public Task Add(Company company);
    public Task<Company?> GetById(Guid id);
}
