using EcommerceLearn.Api.Extensions.Auth;
using EcommerceLearn.Application.Contracts.Order;
using EcommerceLearn.Application.Features.Orders.Commands.PlaceOrder;
using EcommerceLearn.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceLearn.Api.Projection.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("place-order")]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest request, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var command = new PlaceOrderCommand(userId, request.ShippingAddress);


        var result = await _mediator.Send(command, ct);

        if (!result.IsSuccess) return BadRequest(result.Error);

        return Ok();
    }
}