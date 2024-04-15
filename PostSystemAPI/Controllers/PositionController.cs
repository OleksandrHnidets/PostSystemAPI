using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostSystemAPI.Domain.Queries;

namespace PostSystemAPI.WebApi.Controllers;

[Route("api/position")]
[ApiController]
public class PositionController : ControllerBase
{
    private readonly IMediator _mediator;

    public PositionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all-positions")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Viewer")]
    public async Task<IActionResult> GetAllPositions()
    {
        var result = await _mediator.Send(new GetLastPositionsQuery());
        return Ok(result);
    }

    [HttpGet("post-office-locations")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Viewer")]
    public async Task<IActionResult> GetAllPostOfficeLocations()
    {
        var result = await _mediator.Send(new GetAllPostOfficesPositionsQuery());
        return Ok(result);
    }
}