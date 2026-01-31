using MediatR;

namespace EcommerceLearn.Application.Features.Users.Commands.DeleteAddress;

public sealed record DeleteAddressCommand(int UserId, int AddressId) : IRequest<Result>;