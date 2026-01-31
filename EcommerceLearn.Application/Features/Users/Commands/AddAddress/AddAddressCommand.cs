using MediatR;

namespace EcommerceLearn.Application.Features.Users.Commands.AddAddress;

public sealed record AddAddressCommand(int UserId, string Country, string City, string Street, string PostalCode)
    : IRequest<Result>;