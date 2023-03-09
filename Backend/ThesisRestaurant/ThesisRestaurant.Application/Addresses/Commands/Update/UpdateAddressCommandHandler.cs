using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Users.UserAddresses;

namespace ThesisRestaurant.Application.Addresses.Commands.Update
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, ErrorOr<UserAddress>>
    {
        private readonly IUserRepository _userRepository;
        public UpdateAddressCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<UserAddress>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = UserAddress.Create(request.ZipCode, request.City, request.Street, request.HouseNumber, request.AddressId);

            var result = await _userRepository.UpdateAddress(address, request.UserId);

            if (result.IsError) return result.Errors;

            return address;
        }
    }
}
