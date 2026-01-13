using MediatR;
using EcommerceLearn.Domain.Entities.Books;

namespace EcommerceLearn.Application.Features.Books.Queries.GetAllBooks;

public sealed class GetAllBooksQuery() : IRequest<IQueryable<Book>>;