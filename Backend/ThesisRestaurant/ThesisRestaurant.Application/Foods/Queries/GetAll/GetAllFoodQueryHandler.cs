using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Application.Foods.Common;
using ThesisRestaurant.Domain.Foods;

namespace ThesisRestaurant.Application.Foods.Queries.GetAll
{
    public class GetAllFoodQueryHandler : IRequestHandler<GetAllFoodQuery, ErrorOr<List<FoodResult>>>
    {
        private readonly IFoodRepository _foodRepository;
        public GetAllFoodQueryHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<ErrorOr<List<FoodResult>>> Handle(GetAllFoodQuery request, CancellationToken cancellationToken)
        {
            var result = await _foodRepository.GetAllFoods();
            List<FoodResult> retVal = CreateFoodResultList(result);

            return retVal;
        }

        private static List<FoodResult> CreateFoodResultList(List<Food> result)
        {
            List<FoodResult> retVal = new();

            double defaultPrice = double.MaxValue;

            foreach (var food in result)
            {
                var discount = food.GetCurrentPrice();
                retVal.Add(new FoodResult(DiscountPrice: discount is not null ? discount.Price : defaultPrice, Food: food));
            }

            return retVal;
        }
    }
}
