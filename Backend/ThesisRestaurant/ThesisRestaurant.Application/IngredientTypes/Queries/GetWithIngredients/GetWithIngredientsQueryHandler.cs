using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Ingredients;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Application.IngredientTypes.Queries.GetWithIngredients
{
    public class GetWithIngredientsQueryHandler : IRequestHandler<GetWithIngredientsQuery, ErrorOr<List<IngredientType>>>
    {
        private readonly IIngredientTypeRepository _repository;

        public GetWithIngredientsQueryHandler(IIngredientTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<List<IngredientType>>> Handle(GetWithIngredientsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllWithIngredient();
        }
    }
}
