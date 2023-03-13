using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Application.Common.Services;
using ThesisRestaurant.Application.Foods.Common;
using ThesisRestaurant.Domain.Foods;

namespace ThesisRestaurant.Application.Foods.Commands.Create
{
    public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand, ErrorOr<FoodResult>>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IFoodBuilder _foodBuilder;
        public CreateFoodCommandHandler(IFoodRepository foodRepository, IFoodBuilder foodBuilder)
        {
            _foodRepository = foodRepository;
            _foodBuilder = foodBuilder;
        }

        public async Task<ErrorOr<FoodResult>> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
        {
            var food = await  _foodBuilder.Create(request.Name, request.BasePrice, request.FoodTypeId, request.IngredientIds);
            if (food.IsError) return food.Errors;

            var result = await _foodRepository.CreateFood(food.Value);
            if (result.IsError) return result.Errors;

            //itt biztos, hogy nincs még discount, hiszen most jön létre
            return new FoodResult(DiscountPrice: double.MaxValue, food.Value);
        }
    }
}
