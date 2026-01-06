using EcommerceLearn.Domain.Entities.Users;
using MediatR;

namespace EcommerceLearn.Application.Features.Users.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<List<User>>;