using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Entities.Users;
using MediatR;

namespace EcommerceLearn.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IQueryable<User>>
{
    private readonly IDataContext _db;

    public GetAllUsersQueryHandler(IDataContext dataContext)
    {
        _db = dataContext;
    }

    public Task<IQueryable<User>> Handle(GetAllUsersQuery request, CancellationToken ct)
    {
        return Task.FromResult(_db.Users.AsQueryable());
    }
}