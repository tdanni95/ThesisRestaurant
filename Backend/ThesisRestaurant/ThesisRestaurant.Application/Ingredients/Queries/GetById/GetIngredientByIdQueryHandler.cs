using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Application.Ingredients.Queries.GetById
{
    public class GetIngredientByIdQueryHandler : IRequestHandler<GetIngredientByIdQuery, ErrorOr<Ingredient>>
    {
        private readonly IIngredientRepository _repository;

        public GetIngredientByIdQueryHandler(IIngredientRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Ingredient>> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetById(request.Id);
        }
    }
}
