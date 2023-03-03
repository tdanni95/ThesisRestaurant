using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Application.Ingredients.Queries.GetByTypeId
{
    public class GetIngredientByTypeIdQueryHandler : IRequestHandler<GetIngredientByTypeIdQuery, ErrorOr<List<Ingredient>>>
    {
        private readonly IIngredientTypeRepository _ingredientTypeRepository;
        private readonly IIngredientRepository _repository;

        public GetIngredientByTypeIdQueryHandler(IIngredientTypeRepository ingredientTypeRepository, IIngredientRepository repository)
        {
            _ingredientTypeRepository = ingredientTypeRepository;
            _repository = repository;
        }

        public async Task<ErrorOr<List<Ingredient>>> Handle(GetIngredientByTypeIdQuery request, CancellationToken cancellationToken)
        {
            var type = await _ingredientTypeRepository.GetById(request.Id);
            if (type.IsError) return type.Errors;

            var result = await _repository.GetAllByIngredientType(request.Id);
            return result;
        }
    }
}
