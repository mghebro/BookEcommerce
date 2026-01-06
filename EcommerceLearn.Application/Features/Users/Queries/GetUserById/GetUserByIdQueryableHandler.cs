using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Entities.Users;
using MediatR;

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
        var query = _db.Users.Where(e => e.Id == req.Id);

        return Task.FromResult(query);
    }
}