using StockIt.Domain.Entities;
using StockIt.Domain.Repositories;

namespace StockIt.Infra.Data.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task Add(Category category) => await context.Categories.AddAsync(category);
}
