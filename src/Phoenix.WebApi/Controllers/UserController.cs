using Framework.Core.RequestResponse.BaseResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Application.User.Commands.ActivateReservedPlan;
using Phoenix.Application.User.Commands.ClearReservedPlan;
using Phoenix.Application.User.Commands.Create;
using Phoenix.Application.User.Commands.RemoveUserPlan;
using Phoenix.Application.User.Commands.SetUserReservedPlan;
using Phoenix.Application.User.Commands.Update;
using Phoenix.Application.User.Commands.UpdateUserPlan;
using Phoenix.Application.User.DTOs;
using Phoenix.Application.User.Queries.GetById;
using System.Net;

namespace Phoenix.WebApi.Controllers;

[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{

    [HttpPost("create")]
    [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpPut("update-plan")]
    [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdatePlan([FromBody] UpdateUserPlanCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpPut("set-reserved-plan")]
    [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> SetReservedPlan([FromBody] SetUserReservedPlanCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpPut("activate-reserved")]
    [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ActivateReserved([FromBody] ActivateReservedPlanCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpPut("clear-reserved")]
    [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ClearReserved([FromBody] ClearReservedPlanCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpDelete("remove-plan")]
    [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemovePlan([FromBody] RemoveUserPlanCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return Ok(result);
    }
    
    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(ResponseBase<UserDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Ulid userId, CancellationToken ct)
    {
        var result = await mediator.Send(new UserGetByIdQuery(userId), ct);
        return Ok(result);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ListResponseBase<UserDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetList([FromQuery] UserListRequestDto request, CancellationToken ct)
    {
        var query = request.ToQuery();
        var result = await mediator.Send(query, ct);
        return Ok(result);
    }
}