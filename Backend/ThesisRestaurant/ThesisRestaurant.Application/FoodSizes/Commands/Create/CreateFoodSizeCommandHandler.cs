using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Application.IngredientTypes.Common;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Application.FoodSizes.Commands.Create
{
    public class CreateFoodSizeCommandHandler : IRequestHandler<CreateFoodSizeCommand, ErrorOr<FoodSize>>
    {
        private readonly IFoodSizeRepository _repository;
        private readonly IFoodTypeRepository _foodTypeRepository;
        public CreateFoodSizeCommandHandler(IFoodSizeRepository repository, IFoodTypeRepository foodTypeRepository)
        {
            _repository = repository;
            _foodTypeRepository = foodTypeRepository;
        }

        public async Task<ErrorOr<FoodSize>> Handle(CreateFoodSizeCommand request, CancellationToken cancellationToken)
        {
            var type = await _foodTypeRepository.GetById(request.foodTypeId);
            if (type.IsError) return type.Errors;

            var foodSize = FoodSize.Create(request.Name, request.multiplier, type.Value);

            var result = await _repository.Add(foodSize);

            if (result.IsError) return result.Errors;

            return foodSize;
        }
    }
}
