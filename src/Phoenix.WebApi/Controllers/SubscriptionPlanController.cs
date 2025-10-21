using Framework.Core.RequestResponse.BaseResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Application.SubscriptionPlan.Commands.Activate;
using Phoenix.Application.SubscriptionPlan.Commands.Create;
using Phoenix.Application.SubscriptionPlan.Commands.Deactivate;
using Phoenix.Application.SubscriptionPlan.Commands.MarkNotReady;
using Phoenix.Application.SubscriptionPlan.Commands.MarkPending;
using Phoenix.Application.SubscriptionPlan.Commands.Update;
using Phoenix.Application.SubscriptionPlan.DTOs;
using Phoenix.Application.SubscriptionPlan.Queries.GetById;
using System.Net;

namespace Phoenix.WebApi.Controllers;

[Route("api/[controller]")]
public class SubscriptionPlanController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubscriptionPlanController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create")]
    [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Create([FromBody] CreateSubscriptionPlanCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromBody] UpdateSubscriptionPlanCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpPut("{planId}/activate")]
    [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Activate([FromRoute] Ulid planId, CancellationToken ct)
    {
        var result = await _mediator.Send(new ActivateSubscriptionPlanCommand(planId), ct);
        return Ok(result);
    }

    [HttpPut("{planId}/deactivate")]
    [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Deactivate([FromRoute] Ulid planId, CancellationToken ct)
    {
        var result = await _mediator.Send(new DeactivateSubscriptionPlanCommand(planId), ct);
        return Ok(result);
    }

    [HttpPut("{planId}/pending")]
    [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> MarkPending([FromRoute] Ulid planId, CancellationToken ct)
    {
        var result = await _mediator.Send(new MarkPendingSubscriptionPlanCommand(planId), ct);
        return Ok(result);
    }

    [HttpPut("{planId}/not-ready")]
    [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> MarkNotReady([FromRoute] Ulid planId, CancellationToken ct)
    {
        var result = await _mediator.Send(new MarkNotReadySubscriptionPlanCommand(planId), ct);
        return Ok(result);
    }
    [HttpGet("{planId}")]
    [ProducesResponseType(typeof(ResponseBase<SubscriptionPlanDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Ulid planId, CancellationToken ct)
    {
        var result = await _mediator.Send(new SubscriptionPlanGetByIdQuery(planId), ct);
        return Ok(result);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ListResponseBase<SubscriptionPlanDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetList([FromQuery] SubscriptionPlanListRequestDto request, CancellationToken ct)
    {
        var query = request.ToQuery();
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }
    
}