namespace StockIt.Application.DTOs.User;

public record RegisterOwnerRequest(string Name, string Email, string Password, string CompanyName);
