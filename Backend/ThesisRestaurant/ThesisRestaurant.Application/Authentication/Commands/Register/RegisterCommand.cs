using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Authentication.Common;

namespace ThesisRestaurant.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
          string FirstName, string LastName, string Email, string Password, string PasswordAgain, string PhoneNumber, List<RegisterAddressCommand> Addresses
      ) : IRequest<ErrorOr<AuthenticationResult>>;


    public record RegisterAddressCommand(
            int ZipCode, string City, string Street, string HouseNumber
        );
}
