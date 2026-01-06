using EcommerceLearn.Application.Features.Users.Commands.DeleteUser;
using EcommerceLearn.Application.Features.Auth.Commands.Register;
using EcommerceLearn.Application.Contracts.Auth;
using EcommerceLearn.Api.Extensions.Auth;
using EcommerceLearn.Application.Features.Auth.Commands.Login;
using EcommerceLearn.Application.Features.Auth.Commands.ResendVerification;
using EcommerceLearn.Application.Features.Auth.Commands.VerifyEmail;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace EcommerceLearn.Api.Projection.Controllers;

[Route("api/auth")]
[ApiController]
[ProducesResponseType(typeof(ValidationProblemDetails), 400)]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest req, CancellationToken ct)
    {
        var cmd = new RegisterCommand(req.FirstName, req.LastName, req.Email, req.Password);

        var result = await _mediator.Send(cmd, ct);

        return Ok(result);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(CancellationToken ct)
    {
        var userId = this.GetUserId();

        var cmd = new DeleteUserCommand(userId);

        var result = await _mediator.Send(cmd, ct);

        return Ok(result);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest req, CancellationToken ct)
    {
        var cmd = new LoginCommand(req.Email, req.Password);

        var result = await _mediator.Send(cmd, ct);
        return Ok(result);
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail(VerifyEmail req, CancellationToken ct)
    {
        var cmd = new VerifyEmailCommand(req.Email, req.Code);

        var result = await _mediator.Send(cmd, ct);
        return Ok(result);
    }

    [HttpPost("resend-email-code")]
    public async Task<IActionResult> ResendEmailCode(ResendEmailCode req, CancellationToken ct)
    {
        var cmd = new ResendVerificationCommand(req.Email);

        var result = await _mediator.Send(cmd, ct);
        return Ok(result);
    }
}