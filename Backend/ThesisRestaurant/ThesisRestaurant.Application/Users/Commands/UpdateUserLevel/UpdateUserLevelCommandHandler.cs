using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Application.Users.Commands.UpdateUserLevel
{
    public class UpdateUserLevelCommandHandler : IRequestHandler<UpdateUserLevelCommand, ErrorOr<Updated>>
    {
        private readonly IUserRepository _repository;
        public UpdateUserLevelCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Updated>> Handle(UpdateUserLevelCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.ChangeLevel(request.Level, request.Id);

            if (result.IsError) return result.Errors;

            return result;
        }
    }
}
