using MediatR;

namespace EcommerceLearn.Application.Features.Users.Commands.EditAddress;

public sealed record EditAddressCommand(
    int UserId,
    int AddressId,
    string? Country,
    string? City,
    string? Street,
    string? PostalCode,
    bool IsDefault = false)
    : IRequest<Result>;