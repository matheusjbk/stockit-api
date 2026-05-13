using Microsoft.EntityFrameworkCore;
using StockIt.Domain.Entities;
using StockIt.Domain.Repositories;

namespace StockIt.Infra.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;

    public Task<bool> ExistUserWithEmail(string email) => _context.Users.AnyAsync(user => user.Email!.Equals(email));

    public async Task<User?> GetUserByEmail(string email)
    {
        var applicationUser = await _context.Users.FirstOrDefaultAsync(user => user.Email!.Equals(email));

        if (applicationUser is not null) return new User
        {
            Id = applicationUser.Id,
            Name = applicationUser.Name,
            Email = applicationUser.Email!,
            PasswordHash = applicationUser.PasswordHash!
        };

        return null;
    }
}
