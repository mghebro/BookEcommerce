using EcommerceLearn.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using EcommerceLearn.Domain.Common.Results;
using MediatR;

namespace EcommerceLearn.Application.Features.Users.Commands.EditUser;

public sealed class EditUserHandler : IRequestHandler<EditUserCommand, Result>
{
    private readonly IDataContext _db;

    public EditUserHandler(IDataContext db) => _db = db;

    public async Task<Result> Handle(EditUserCommand req, CancellationToken ct)
    {
        var user = await _db.Users.FirstOrDefaultAsync(e => e.Id == req.UserId, ct);

        if (user is null) return Result.Failure(Errors.NotFound("User not found!"));

        user.Rename(req.FirstName, req.LastName);

        await _db.SaveChangesAsync(ct);

        return Result.Success();
    }
}