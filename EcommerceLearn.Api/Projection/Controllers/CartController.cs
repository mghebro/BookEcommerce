using EcommerceLearn.Api.Extensions.Auth;
using EcommerceLearn.Application.Features.Carts.Queries.GetCartByUserId;
using EcommerceLearn.Application.Features.Carts.Commands.AddToCart;
using EcommerceLearn.Application.Features.Carts.Commands.RemoveFromCart;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using EcommerceLearn.Application.Contracts.Cart;

namespace EcommerceLearn.Api.Projection.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("add")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userId = this.GetUserId();
        var command = new AddToCartCommand(userId, request.BookId, request.Quantity);

        var result = await _mediator.Send(command, ct);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error?.Message });

        var cartResult = await _mediator.Send(new GetCartByUserIdQuery(userId), ct);
        return Ok(cartResult);
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userId = this.GetUserId();
        var command = new RemoveFromCartCommand(userId, request.BookId, request.QuantityToRemove);
        var result = await _mediator.Send(command, ct);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error?.Message });

        var cartResult = await _mediator.Send(new GetCartByUserIdQuery(userId), ct);
        return Ok(cartResult);
    }
}