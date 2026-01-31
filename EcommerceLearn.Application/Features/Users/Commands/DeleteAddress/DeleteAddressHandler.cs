using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Users.Commands.DeleteAddress;

public sealed class DeleteAddressHandler : IRequestHandler<DeleteAddressCommand, Result>
{
    private readonly IDataContext _db;

    public DeleteAddressHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<Result> Handle(DeleteAddressCommand req, CancellationToken ct)
    {
        var address =
            await _db.UserAddresses.FirstOrDefaultAsync(e => e.Id == req.AddressId && e.UserId == req.UserId, ct);

        if (address is null) return Result.Failure(Errors.NotFound("Address not found!"));
        if (address.IsDefault) address.UnmarkAsDefault();

        address.Delete();
        await _db.SaveChangesAsync(ct);
        return Result.Success();
    }
}