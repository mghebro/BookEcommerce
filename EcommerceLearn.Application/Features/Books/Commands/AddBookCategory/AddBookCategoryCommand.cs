using EcommerceLearn.Domain.Enums.Books;
using MediatR;

namespace EcommerceLearn.Application.Features.Books.Commands.AddBookCategory;

public sealed record AddBookCategoryCommand(int BookId, Category Category) : IRequest<Result>;