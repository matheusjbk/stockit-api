using StockIt.Domain.Entities;

namespace StockIt.Domain.Repositories;

public interface ICategoryRepository
{
    public Task Add(Category category);
}
