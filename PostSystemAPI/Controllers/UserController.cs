using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostSystemAPI.Domain.Commands;
using PostSystemAPI.Domain.Queries;

namespace PostSystemAPI.WebApi.Controllers;


[Route("api/driver")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("{driverId}/add-delivery/{deliveryId}")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles ="Admin")]
    public async Task<ActionResult<string>> AddDeliveryToDriver(string driverId, string deliveryId)
    {
        var result = await _mediator.Send(new AssignDeliveryToDriverCommand(driverId, Guid.Parse(deliveryId)));
        return Ok(result);
    }

    [HttpGet("{driverId}/deliveries")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles ="Driver")]
    public async Task<IActionResult> GetDriverDeliveries(string driverId)
    {
        var result = await _mediator.Send(new GetDriverDeliveries(driverId));
        return Ok(result);
    }
}