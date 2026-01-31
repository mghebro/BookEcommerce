using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Users.Queries.GetUserById;

public sealed class GetUserByIdQueryableHandler : IRequestHandler<GetUserByIdQueryable, IQueryable<User>>
{
    private readonly IDataContext _db;

    public GetUserByIdQueryableHandler(IDataContext db)
    {
        _db = db;
    }

    public Task<IQueryable<User>> Handle(GetUserByIdQueryable req, CancellationToken ct)
    {
        var userQuery = _db.Users.Where(u => u.Id == req.Id).Include(u => u.UserAddresses);
        return Task.FromResult(userQuery.AsQueryable());
    }
}