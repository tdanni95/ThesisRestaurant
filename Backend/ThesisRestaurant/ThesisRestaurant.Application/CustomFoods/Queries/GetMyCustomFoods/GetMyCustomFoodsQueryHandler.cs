using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.CustomFoods;

namespace ThesisRestaurant.Application.CustomFoods.Queries.GetMyCustomFoods
{
    public class GetMyCustomFoodsQueryHandler : IRequestHandler<GetMyCustomFoodsQuery, ErrorOr<List<CustomFood>>>
    {
        private readonly ICustomFoodRepository _repository;

        public GetMyCustomFoodsQueryHandler(ICustomFoodRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<List<CustomFood>>> Handle(GetMyCustomFoodsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetUserCustomFoods(request.UserId);
            return result;
        }
    }
}
