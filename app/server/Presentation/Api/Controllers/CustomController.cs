
using Articly.Presentation.Api.Response;
using Microsoft.AspNetCore.Mvc;

namespace Articly.Presentation.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class CustomController : ControllerBase
{
    public CustomController() { }

    protected ActionResult CustomResponse<T>(T result)
    {
        return Ok(result);
    }

    protected ActionResult CustomResponseException(Exception exception)
    {
        var response = BaseResponseAPI.Create(
            GetInnerExceptionMessages(exception), false
        );
        
        return BadRequest(response);
    }

    private string GetInnerExceptionMessages(Exception ex)
    {
        var messages = ex.Message;
        var innerException = ex.InnerException;
        
        while (innerException != null)
        {
            messages += " --> " + innerException.Message;
            innerException = innerException.InnerException;
        }

        return messages;
    }
}