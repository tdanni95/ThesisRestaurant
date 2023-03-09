using ErrorOr;
using MediatR;

namespace ThesisRestaurant.Application.Addresses.Commands.Delete
{
    public record DeleteAddressCommand(int userId, int addressId) : IRequest<ErrorOr<Deleted>>
    {
    }
}
