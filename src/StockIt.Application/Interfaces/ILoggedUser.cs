namespace StockIt.Application.Interfaces;

public interface ILoggedUser
{
    Guid GetUserEmail();
    Guid GetCompanyId();
}
