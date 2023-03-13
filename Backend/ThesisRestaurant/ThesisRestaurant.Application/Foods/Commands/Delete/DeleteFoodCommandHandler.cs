using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;

namespace ThesisRestaurant.Application.Foods.Commands.Delete
{
    public class DeleteFoodCommandHandler : IRequestHandler<DeleteFoodCommand, ErrorOr<Deleted>>
    {
        private readonly IFoodRepository _foodRepository;
        public DeleteFoodCommandHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
        {
            var result = await _foodRepository.Delete(request.id);

            return result;
        }
    }
}
