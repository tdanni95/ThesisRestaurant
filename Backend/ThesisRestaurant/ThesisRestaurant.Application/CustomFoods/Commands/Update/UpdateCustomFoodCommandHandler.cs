using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Application.Common.Services;
using ThesisRestaurant.Domain.CustomFoods;

namespace ThesisRestaurant.Application.CustomFoods.Commands.Update
{
    public class UpdateCustomFoodCommandHandler : IRequestHandler<UpdateCustomFoodCommand, ErrorOr<CustomFood>>
    {
        private readonly ICustomFoodRepository _customFoodRepository;
        private readonly ICustomFoodBuilder _customFoodBuilder;

        public UpdateCustomFoodCommandHandler(ICustomFoodRepository customFoodRepository, ICustomFoodBuilder customFoodBuilder)
        {
            _customFoodRepository = customFoodRepository;
            _customFoodBuilder = customFoodBuilder;
        }

        public async Task<ErrorOr<CustomFood>> Handle(UpdateCustomFoodCommand request, CancellationToken cancellationToken)
        {
            var customFood = await _customFoodBuilder.Create(request.Name, request.FoodTypeId, request.IngredientIds, request.Id);
            if (customFood.IsError) return customFood.Errors;

            var result = await _customFoodRepository.UpdateCustomFood(customFood.Value, request.UserId);
            if (result.IsError) return result.Errors;

            return customFood;
        }

    }
}
