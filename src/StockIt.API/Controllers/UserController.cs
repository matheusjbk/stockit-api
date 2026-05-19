using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockIt.Application.DTOs.User;
using StockIt.Application.UseCases.User.Profile;
using StockIt.Application.UseCases.User.Register.Employee;
using StockIt.Application.UseCases.User.Register.Owner;

namespace StockIt.API.Controllers;
public class UserController : StockItBaseController
{
    [HttpPost("owner")]
    [ProducesResponseType(typeof(RegisteredUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterOwner(RegisterUserRequest request, IRegisterOwnerUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return HandleCreated(result);
    }

    [Authorize(Roles = "Owner")]
    [HttpPost("employee")]
    [ProducesResponseType(typeof(RegisteredUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterEmployee(RegisterUserRequest request, IRegisterEmployeeUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return HandleCreated(result);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(UserProfileResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Profile(IGetUserProfileUseCase useCase)
    {
        var result = await useCase.Execute();

        return HandleResult(result);
    }
}
