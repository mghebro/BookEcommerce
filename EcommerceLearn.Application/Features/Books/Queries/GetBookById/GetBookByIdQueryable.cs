using EcommerceLearn.Domain.Entities.Books;
using MediatR;

namespace EcommerceLearn.Application.Features.Books.Queries.GetBookById;

public sealed record GetBookByIdQueryable(int Id) : IRequest<IQueryable<Book>>;