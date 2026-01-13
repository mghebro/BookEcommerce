using EcommerceLearn.Application.Features.Books.Commands.CreateBook;
using EcommerceLearn.Application.Contracts.Book;
using EcommerceLearn.Api.Extensions.Auth;
using EcommerceLearn.Application.Features.Books.Commands.DeleteBook;
using EcommerceLearn.Application.Features.Books.Commands.UpdateBook;
using EcommerceLearn.Domain.Enums.Books;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace EcommerceLearn.Api.Projection.Controllers;

[Route("api/books")]
[ApiController]
[ProducesResponseType(typeof(ValidationProblemDetails), 400)]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost("create-book")]
    public async Task<IActionResult> Register(CreateBookRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var cmd = new CreateBookCommand(req.Title, req.Description, req.Isbn, req.PageCount, req.CoverImageUrl,
            req.AuthorFullname,
            req.Language, req.Price, new List<Category>());

        var result = await _mediator.Send(cmd, ct);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("update-book")]
    public async Task<IActionResult> Update(UpdateBookRequest req, CancellationToken ct)
    {
        var cmd = new UpdateBookCommand(req.Id, req.Title, req.Description, req.PageCount, req.CoverImageUrl,
            req.Language, req.Price);

        var result = await _mediator.Send(cmd, ct);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("delete-book")]
    public async Task<IActionResult> Delete(DeleteBookRequest req, CancellationToken ct)
    {
        var cmd = new DeleteBookCommand(req.Id);

        var result = await _mediator.Send(cmd, ct);
        return Ok(result);
    }
}