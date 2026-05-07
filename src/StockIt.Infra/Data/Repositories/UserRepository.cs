using Microsoft.EntityFrameworkCore;
using StockIt.Domain.Repositories;

namespace StockIt.Infra.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;

    public Task<bool> ExistUserWithEmail(string email) => _context.Users.AnyAsync(user => user.Email!.Equals(email));
}
