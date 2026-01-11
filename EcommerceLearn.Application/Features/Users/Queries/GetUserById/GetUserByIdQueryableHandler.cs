using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Users.Queries.GetUserById;

public sealed class GetUserByIdQueryableHandler : IRequestHandler<GetUserByIdQueryable, Result<User>>
{
    private readonly IDataContext _db;

    public GetUserByIdQueryableHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<Result<User>> Handle(GetUserByIdQueryable req, CancellationToken ct)
    {
        var user = await _db.Users.FirstOrDefaultAsync(e => e.Id == req.Id, ct);
        if (user == null)
            return Result<User>.Failure(Errors.NotFound("User not found"));
        return Result<User>.Success(user);
    }
}