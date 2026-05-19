namespace StockIt.Application.Security;

public record AuthenticatedUser(string Email, Guid CompanyId, string Role);
