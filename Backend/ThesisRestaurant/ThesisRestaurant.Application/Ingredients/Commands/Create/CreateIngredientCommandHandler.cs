using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.Ingredients;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Application.Ingredients.Commands.Create
{
    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, ErrorOr<Ingredient>>
    {
        private readonly IIngredientRepository _repository;
        private readonly IIngredientTypeRepository _ingredientTypeRepository;
        public CreateIngredientCommandHandler(IIngredientRepository repository, IIngredientTypeRepository ingredientTypeRepository)
        {
            _repository = repository;
            _ingredientTypeRepository= ingredientTypeRepository;
            
        }

        public async Task<ErrorOr<Ingredient>> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            ErrorOr<IngredientType> ingredientType = await _ingredientTypeRepository.GetById(request.IngredientTypeId);
            if (ingredientType.IsError) return ingredientType.Errors;

            var ingredient = Ingredient.Create(request.Name,
                                               request.Price,
                                               ingredientType.Value);

            var result = await _repository.Add(ingredient);
            if (result.IsError) return result.Errors;

            return ingredient;
        }
    }
}
