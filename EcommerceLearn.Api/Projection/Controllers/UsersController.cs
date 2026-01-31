using EcommerceLearn.Api.Extensions.Auth;
using EcommerceLearn.Application.Features.Users.Commands.EditUser;
using EcommerceLearn.Application.Contracts.Users.Requests;
using EcommerceLearn.Application.Features.Users.Commands.AddAddress;
using EcommerceLearn.Application.Features.Users.Commands.DeleteAddress;
using EcommerceLearn.Application.Features.Users.Commands.EditAddress;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace EcommerceLearn.Api.Projection.Controllers;

[Route("api/users")]
[ApiController]
[ProducesResponseType(typeof(ValidationProblemDetails), 400)]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPut("edit")]
    public async Task<IActionResult> EditUser(EditUserRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();

        var cmd = new EditUserCommand(req.FirstName, req.LastName, userId);

        var result = await _mediator.Send(cmd, ct);

        return Ok(result);
    }

    [HttpPost("create-address")]
    public async Task<IActionResult> CreateAddress(AddAddressRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();

        var cmd = new AddAddressCommand(userId, req.Country, req.City, req.Street, req.PostalCode);

        var result = await _mediator.Send(cmd, ct);

        return Ok(result);
    }

    [HttpPut("edit-address")]
    public async Task<IActionResult> EditAddress(EditAddressRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();

        var cmd = new EditAddressCommand(userId, req.AddressId, req.Country, req.City, req.Street, req.PostalCode,
            req.IsDefault);

        var result = await _mediator.Send(cmd, ct);

        return Ok(result);
    }

    [HttpDelete("delete-address")]
    public async Task<IActionResult> DeleteAddress(DeleteAddressRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();

        var cmd = new DeleteAddressCommand(userId, req.AddressId);

        var result = await _mediator.Send(cmd, ct);

        return Ok(result);
    }
}