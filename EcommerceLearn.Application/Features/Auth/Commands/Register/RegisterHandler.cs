using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Application.Features.Auth.Services;
using EcommerceLearn.Domain.Entities.Carts;
using EcommerceLearn.Domain.Entities.Users;
using MediatR;

namespace EcommerceLearn.Application.Features.Auth.Commands.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, Result>
{
    private readonly IDataContext _db;
    private readonly IAuthEmailService _email;

    public RegisterHandler(IDataContext db, IAuthEmailService email)
    {
        _db = db;
        _email = email;
    }

    public async Task<Result> Handle(RegisterCommand req, CancellationToken ct)
    {
        var userResult = User.Create(req.FirstName, req.LastName, req.Email);

        if (!userResult.IsSuccess) return Result.Failure(userResult.Error!);

        var user = userResult.Value!;


        user.SetPassword(BCrypt.Net.BCrypt.HashPassword(req.Password));

        var verification = await _email.SendEmailVerification(user);
        user.SetEmailVerification(verification);
        user.CreateCart();

        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);

        return Result.Success();
    }
}