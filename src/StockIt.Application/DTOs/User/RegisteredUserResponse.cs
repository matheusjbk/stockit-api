namespace StockIt.Application.DTOs.User;

public record RegisteredUserResponse(string Name, string Email, Guid CompanyId);
