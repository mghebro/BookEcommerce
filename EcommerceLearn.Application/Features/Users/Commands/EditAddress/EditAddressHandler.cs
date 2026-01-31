using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Users.Commands.EditAddress;

public sealed class EditAddressHandler : IRequestHandler<EditAddressCommand, Result>
{
    private readonly IDataContext _db;

    public EditAddressHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<Result> Handle(EditAddressCommand req, CancellationToken ct)
    {
        var address = await _db.UserAddresses
            .FirstOrDefaultAsync(a => a.Id == req.AddressId && a.UserId == req.UserId, ct);

        if (address is null)
            return Result.Failure(Errors.NotFound("Address not found!"));

        address.Update(req.Country, req.City, req.Street, req.PostalCode);

        if (req.IsDefault)
        {
            var otherAddresses = await _db.UserAddresses
                .Where(a => a.UserId == req.UserId && a.Id != req.AddressId)
                .ToListAsync(ct);

            foreach (var a in otherAddresses)
                a.UnmarkAsDefault();

            address.MarkAsDefault();
        }
        else
        {
            address.UnmarkAsDefault();
        }

        await _db.SaveChangesAsync(ct);
        return Result.Success();
    }
}