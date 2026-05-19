using Microsoft.AspNetCore.Http;
using StockIt.Application.Services;
using System.Security.Claims;

namespace StockIt.Infra.Services;

internal class LoggedUser(IHttpContextAccessor contextAccessor) : ILoggedUser
{
    public Guid GetUserEmail()
    {
        var userId = contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Sid);

        return userId != null ? Guid.Parse(userId) : Guid.Empty;
    }

    public Guid GetCompanyId()
    {
        var companyId = contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.GroupSid);

        return companyId != null ? Guid.Parse(companyId) : Guid.Empty;
    }
}
