using Articly.Core.Application.Stats;
using Articly.Presentation.Api.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Articly.Presentation.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("Api")]
[Authorize]
public class StatController : CustomController
{
    #region Properties
    private readonly IMediator mediator;
    #endregion

    #region Constructors
    public StatController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    #endregion

    #region Actions

    [HttpGet]
    public async Task<ActionResult> GetLast()
    {
        var query = new GetLastStatQuery();
        var dtos  = await mediator.Send(query);
        var response = BaseResponseAPI.Create(dtos);
        
        return CustomResponse(response);
    }
    #endregion
}
