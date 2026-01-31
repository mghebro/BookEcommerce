using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Users.Commands.AddAddress;

public sealed class AddAddressHandler : IRequestHandler<AddAddressCommand, Result>
{
    private readonly IDataContext _db;

    public AddAddressHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<Result> Handle(AddAddressCommand req, CancellationToken ct)
    {
        var user = await _db.Users.FirstOrDefaultAsync(e => e.Id == req.UserId, ct);

        if (user is null) return Result.Failure(Errors.NotFound("User not found!"));
        user.AddAddress(req.Country, req.City, req.Street, req.PostalCode);
        await _db.SaveChangesAsync(ct);
        return Result.Success();
    }
}