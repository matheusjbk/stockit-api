using Microsoft.EntityFrameworkCore;
using StockIt.Domain.Entities;
using StockIt.Domain.Repositories;

namespace StockIt.Infra.Data.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{

    public Task<bool> ExistUserWithEmail(string email) => context.Users.AnyAsync(user => user.Email!.Equals(email));

    public async Task<User?> GetUserByEmail(string email)
    {
        var applicationUser = await context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email!.Equals(email));

        if (applicationUser is not null) return new User
        {
            Id = applicationUser.Id,
            Name = applicationUser.Name,
            Email = applicationUser.Email!,
            PasswordHash = applicationUser.PasswordHash!,
            CompanyId = applicationUser.CompanyId,
        };

        return null;
    }
}
