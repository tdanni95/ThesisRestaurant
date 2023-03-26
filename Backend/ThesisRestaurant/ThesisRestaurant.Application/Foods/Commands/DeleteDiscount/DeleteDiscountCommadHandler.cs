using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;

namespace ThesisRestaurant.Application.Foods.Commands.DeleteDiscount
{
    public class DeleteDiscountCommadHandler : IRequestHandler<DeleteDiscountCommad, ErrorOr<Deleted>>
    {
        private readonly IFoodRepository _foodRepository;
        public DeleteDiscountCommadHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }
        public async Task<ErrorOr<Deleted>> Handle(DeleteDiscountCommad request, CancellationToken cancellationToken)
        {
            var result = await _foodRepository.DeleteDiscount(request.id);
            return result;
        }
    }
}
