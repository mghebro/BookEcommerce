using EcommerceLearn.Application.Features.Users.Commands.DeleteUser;
using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Users.Commands;

public sealed class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IDataContext  _db;
    
    public DeleteUserHandler(IDataContext db) => _db = db;
    
    public async Task<Result> Handle(DeleteUserCommand req, CancellationToken ct)
    {
        var user = await _db.Users.FirstOrDefaultAsync(e => e.Id == req.UserId, ct);

        if (user is null) return Result.Failure(Errors.NotFound("User not found!"));
        
        _db.Users.Remove(user);
        await _db.SaveChangesAsync(ct);

        return Result.Success();
    }
}