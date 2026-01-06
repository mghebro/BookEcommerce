using MediatR;

namespace EcommerceLearn.Application.Features.Books.Commands.DeleteBook;

public sealed record DeleteBookCommand(int Id) : IRequest<Result>;