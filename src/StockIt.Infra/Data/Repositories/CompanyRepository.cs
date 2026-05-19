using Microsoft.EntityFrameworkCore;
using StockIt.Domain.Entities;
using StockIt.Domain.Repositories;

namespace StockIt.Infra.Data.Repositories;

public class CompanyRepository(AppDbContext context) : ICompanyRepository
{
    public async Task Add(Company company) => await context.Companies.AddAsync(company);

    public async Task<Company?> GetById(Guid id) => await context.Companies.FirstOrDefaultAsync(company => company.Id.Equals(id));
}
