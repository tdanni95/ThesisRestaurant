using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Application.Foods.Common;
using ThesisRestaurant.Domain.Common.Errors;

namespace ThesisRestaurant.Application.Foods.Queries.GetById
{
    public class GetFoodByIdQueryHandler : IRequestHandler<GetFoodByIdQuery, ErrorOr<FoodResult>>
    {
        private readonly IFoodRepository _foodRepository;

        public GetFoodByIdQueryHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<ErrorOr<FoodResult>> Handle(GetFoodByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _foodRepository.GetFoodById(request.Id);
            if (result is null) return Errors.Foods.NotFound;

            var price = result.GetCurrentPrice();

            return new FoodResult(DiscountPrice: price is not null ? price.Price : double.MaxValue, Food: result);
        }
    }
}
