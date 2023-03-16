using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ErrorOr<User>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);
            if (user is null) return Errors.Users.NotFound;
            return user;
        }
    }
}
