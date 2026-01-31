using EcommerceLearn.Domain.Entities.Users;
using MediatR;

namespace EcommerceLearn.Application.Features.Users.Queries.GetUserById;

public sealed record GetUserByIdQueryable(int Id) : IRequest<IQueryable<User>>;