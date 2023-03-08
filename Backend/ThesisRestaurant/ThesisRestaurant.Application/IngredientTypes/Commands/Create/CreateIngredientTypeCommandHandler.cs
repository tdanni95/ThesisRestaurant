using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Application.IngredientTypes.Common;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Application.IngredientTypes.Commands.Create
{
    public class CreateIngredientTypeCommandHandler : IRequestHandler<CreateIngredientTypeCommand, ErrorOr<IngredientType>>
    {
        private readonly IIngredientTypeRepository _repository;
        public CreateIngredientTypeCommandHandler(IIngredientTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<IngredientType>> Handle(CreateIngredientTypeCommand request, CancellationToken cancellationToken)
        {
            var ingredientType = IngredientType.Create(request.Name, request.MinOption, request.MaxOption);

            var result = await _repository.Add(ingredientType);

            if (result.IsError) return result.Errors;

            return ingredientType;
        }
    }
}
