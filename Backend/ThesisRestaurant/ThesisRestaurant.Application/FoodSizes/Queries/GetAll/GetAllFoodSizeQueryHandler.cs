using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;

namespace ThesisRestaurant.Application.FoodSizes.Queries.GetAll
{
    public class GetAllFoodSizeQueryHandler : IRequestHandler<GetAllFoodSizeQuery, ErrorOr<List<FoodSize>>>
    {
        private readonly IFoodSizeRepository _repository;
        public GetAllFoodSizeQueryHandler(IFoodSizeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<List<FoodSize>>> Handle(GetAllFoodSizeQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll();
        }
    }
}
