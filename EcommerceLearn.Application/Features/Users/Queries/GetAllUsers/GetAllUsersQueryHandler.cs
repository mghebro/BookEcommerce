using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using MediatR;


namespace EcommerceLearn.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
{
    private readonly IDataContext _db;

    public GetAllUsersQueryHandler(IDataContext dataContext)
    {
        _db = dataContext;
    }

    public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken ct)
    {
        return await _db.Users.ToListAsync(ct);
    }
}