namespace StockIt.Domain.Security;

public record AuthenticatedUser(string Email, Guid CompanyId, string Role);
