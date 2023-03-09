using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Users.UserAddresses;

namespace ThesisRestaurant.Application.Addresses.Commands.Create
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, ErrorOr<UserAddress>>
    {
        private readonly IUserRepository _repository;

        public CreateAddressCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<UserAddress>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = UserAddress.Create(request.ZipCode, request.City, request.Street, request.HouseNumber);
            var result = await _repository.AddAddress(address, request.UserId);
            if (result.IsError) return result.Errors;

            return address;
        }
    }
}
