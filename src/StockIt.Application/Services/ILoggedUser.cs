namespace StockIt.Application.Services;

public interface ILoggedUser
{
    string GetUserEmail();
    Guid GetCompanyId();
    string GetUserRole();
}
