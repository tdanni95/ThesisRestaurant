using ErrorOr;
using MediatR;
using ThesisRestaurant.Domain.Users.UserAddresses;

namespace ThesisRestaurant.Application.Addresses.Commands.Create
{
    public record CreateAddressCommand(int UserId, int ZipCode, string City, string Street, string HouseNumber) : IRequest<ErrorOr<UserAddress>>;
}
