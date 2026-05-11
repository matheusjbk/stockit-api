using Microsoft.AspNetCore.Http;
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

    private ObjectResult HandleError(IError error)
    {
        IEnumerable<string> errorMessages;
        if (error is AggregateError aggregateError)
        {
            errorMessages = aggregateError.SubErrors.SelectMany(e => e.Messages);

        }
        else
        {
            errorMessages = error.Messages;
        }

        return StatusCode(error.StatusCode, new
        {
            Status = error.StatusCode,
            Detail = errorMessages
        });
    }
}
