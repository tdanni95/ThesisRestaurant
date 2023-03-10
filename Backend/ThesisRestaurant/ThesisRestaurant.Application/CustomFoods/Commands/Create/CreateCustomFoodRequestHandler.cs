using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Services;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.CustomFoods;

namespace ThesisRestaurant.Application.CustomFoods.Commands.Create
{
    public class CreateCustomFoodRequestHandler : IRequestHandler<CreateCustomFoodCommand, ErrorOr<CustomFood>>
    {

        private readonly ICustomFoodRepository _customFoodRepository;
        private readonly ICustomFoodBuilder _customFoodBuilder;

        public CreateCustomFoodRequestHandler(
            ICustomFoodBuilder customFoodBuilder,
            ICustomFoodRepository customFoodRepository)
        {
            _customFoodBuilder = customFoodBuilder;
            _customFoodRepository = customFoodRepository;
        }

        public async Task<ErrorOr<CustomFood>> Handle(CreateCustomFoodCommand request, CancellationToken cancellationToken)
        {
            var customFood = await _customFoodBuilder.Create(request.Name, request.FoodTypeId, request.IngredientIds);
            if (customFood.IsError) return customFood.Errors;

            var result = await _customFoodRepository.AddCustomFood(customFood.Value, request.UserId);
            if (result.IsError) return result.Errors;

            return customFood;
        }


    }
}

