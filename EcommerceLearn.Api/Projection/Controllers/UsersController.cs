using EcommerceLearn.Api.Extensions.Auth;
using EcommerceLearn.Application.Features.Users.Commands.EditUser;
using EcommerceLearn.Application.Contracts.Users.Requests;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace EcommerceLearn.Api.Projection.Controllers
{
    [Route("api/users"), ApiController]
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

       
        [HttpPut("edit")]
        public async Task<IActionResult> EditUser( EditUserRequest req, CancellationToken ct)
        {
            int userId = this.GetUserId();
            
            var cmd = new EditUserCommand(req.FirstName, req.LastName , userId);

            var result = await _mediator.Send(cmd, ct);

            return Ok(result);
        }
    }
}