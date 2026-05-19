namespace StockIt.Application.Services;

public interface ILoggedUser
{
    Guid GetUserEmail();
    Guid GetCompanyId();
}
