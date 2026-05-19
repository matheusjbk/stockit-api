using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockIt.Application.DTOs.Category;
using StockIt.Application.UseCases.Category.Register;

namespace StockIt.API.Controllers;

[Authorize]
public class CategoryController : StockItBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(CategoryRequest request, IRegisterCategoryUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return HandleCreated(result);
    }
}
