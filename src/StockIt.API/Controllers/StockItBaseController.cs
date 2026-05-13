using Microsoft.AspNetCore.Mvc;
using StockIt.Domain.Shared;
using StockIt.Domain.Shared.Errors;

namespace StockIt.API.Controllers;

[Route("[controller]")]
[ApiController]
public class StockItBaseController : ControllerBase
{
    protected IActionResult HandleResult(Result result)
    {
        if(result.IsSuccess) return NoContent();

        return HandleError(result.Error!);
    }

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if(result.IsSuccess) return Ok(result.Value);

        return HandleError(result.Error!);
    }

    protected IActionResult HandleCreated<T>(Result<T> result)
    {
        if (result.IsSuccess) return Created(string.Empty, result.Value);

        return HandleError(result.Error!);
    }

    private static ObjectResult HandleError(IError error)
    {
        var errorMessages = error is AggregateError aggregateError
            ? aggregateError.SubErrors.SelectMany(e => e.Messages)
            : error.Messages;

        var problemDetails = new ProblemDetails
        {
            Status = error.StatusCode,
            Detail = string.Join(" | ", errorMessages),
            Title = ErrorMessages.PROBLEM_DETAILS_TITLE
        };

        return new ObjectResult(problemDetails){
            StatusCode = error.StatusCode,
        };
    }
}
