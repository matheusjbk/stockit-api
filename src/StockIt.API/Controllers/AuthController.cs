using Microsoft.AspNetCore.Mvc;
using StockIt.Application.DTOs.Login;
using StockIt.Application.UseCases.Auth.Login;

namespace StockIt.API.Controllers;
public class AuthController : StockItBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LoginRequest request, IDoLoginUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return HandleResult(result);
    }
}
