using Microsoft.AspNetCore.Http;
using StockIt.Application.Services;
using System.Security.Claims;

namespace StockIt.Infra.Services;

internal class LoggedUser(IHttpContextAccessor contextAccessor) : ILoggedUser
{
    public string GetUserEmail()
    {
        var userEmail = contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Sid);

        return userEmail ?? string.Empty;
    }

    public Guid GetCompanyId()
    {
        var companyId = contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.GroupSid);

        return companyId != null ? Guid.Parse(companyId) : Guid.Empty;
    }

    public string GetUserRole()
    {
        var userRole = contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

        return userRole ?? string.Empty;
    }
}
