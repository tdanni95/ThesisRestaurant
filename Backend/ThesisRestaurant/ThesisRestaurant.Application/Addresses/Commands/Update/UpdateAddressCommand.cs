using ErrorOr;
using MediatR;
using ThesisRestaurant.Domain.Users.UserAddresses;

namespace ThesisRestaurant.Application.Addresses.Commands.Update
{
    public record UpdateAddressCommand(int UserId, int AddressId, int ZipCode, string City, string Street, string HouseNumber) : IRequest<ErrorOr<UserAddress>>;
}
