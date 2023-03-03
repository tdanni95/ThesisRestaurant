using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Ingredients;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Application.Ingredients.Commands.Update
{
    public class UpdateIngredientCommandHandler : IRequestHandler<UpdateIngredientCommand, ErrorOr<Ingredient>>
    {
        private readonly IIngredientRepository _repository;
        private readonly IIngredientTypeRepository _ingredientTypeRepository;

        public UpdateIngredientCommandHandler(IIngredientRepository repository, IIngredientTypeRepository ingredientTypeRepository)
        {
            _repository = repository;
            _ingredientTypeRepository = ingredientTypeRepository;
        }

        public async Task<ErrorOr<Ingredient>> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            ErrorOr<IngredientType> ingredientType = await _ingredientTypeRepository.GetById(request.IngredientTypeId);
            if (ingredientType.IsError) return ingredientType.Errors;

            var ingredient = Ingredient.Create(request.Name,
                                               request.Price,
                                               ingredientType.Value,
                                               request.Id);

            var result = await _repository.Update(ingredient);
            if (result.IsError) return result.Errors;

            return ingredient;
        }
    }
}
