using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;

namespace ThesisRestaurant.Application.Addresses.Commands.Delete
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, ErrorOr<Deleted>>
    {
        private readonly IUserRepository _userRepository;
        public DeleteAddressCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.DeleteAddress(request.addressId, request.userId);
            return result;
        }
    }
}
