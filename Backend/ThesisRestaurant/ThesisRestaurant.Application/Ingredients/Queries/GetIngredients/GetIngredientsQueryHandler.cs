using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Application.Ingredients.Queries.GetIngredients
{
    public class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQuery, ErrorOr<List<Ingredient>>>
    {
        private readonly IIngredientRepository _repository;

        public GetIngredientsQueryHandler(IIngredientRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<List<Ingredient>>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll();
        }
    }
}
