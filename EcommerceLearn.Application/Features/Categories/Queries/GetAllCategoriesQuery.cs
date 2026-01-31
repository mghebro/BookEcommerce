using EcommerceLearn.Domain.Entities.Books;
using EcommerceLearn.Domain.Enums.Books;
using MediatR;

namespace EcommerceLearn.Application.Features.Categories.Queries;

public sealed record GetAllCategoriesQuery : IRequest<IQueryable<Category>>;