using Microsoft.AspNetCore.Mvc;
using StockIt.Application.DTOs.User;
using StockIt.Application.UseCases.User.Register.Owner;

namespace StockIt.API.Controllers;
public class UserController : StockItBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(RegisteredUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(RegisterOwnerRequest request, IRegisterOwnerUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return HandleCreated(result);
    }
}
