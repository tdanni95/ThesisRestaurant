using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Application.Common.Services;
using ThesisRestaurant.Application.Foods.Common;

namespace ThesisRestaurant.Application.Foods.Commands.Update
{
    public class UpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand, ErrorOr<FoodResult>>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IFoodBuilder _foodBuilder;
        public UpdateFoodCommandHandler(IFoodRepository foodRepository, IFoodBuilder foodBuilder)
        {
            _foodRepository = foodRepository;
            _foodBuilder = foodBuilder;
        }

        public async Task<ErrorOr<FoodResult>> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
        {
            var food = await _foodBuilder.Create(request.Name, request.BasePrice, request.FoodTypeId, request.IngredientIds, request.Id);
            if (food.IsError) return food.Errors;

            var result = await _foodRepository.Update(food.Value);
            if (result.IsError) return result.Errors;

            //itt biztos, hogy nincs még discount, hiszen most jön létre
            return new FoodResult(DiscountPrice: double.MaxValue, food.Value);
        }
    }
}
