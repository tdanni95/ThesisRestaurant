using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.CustomFoods;

namespace ThesisRestaurant.Application.CustomFoods.Queries.GetById
{
    public class GetCustomFoodByIdQueryHandler : IRequestHandler<GetCustomFoodByIdQuery, ErrorOr<CustomFood>>
    {
        private readonly ICustomFoodRepository _repository;
        public GetCustomFoodByIdQueryHandler(ICustomFoodRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<CustomFood>> Handle(GetCustomFoodByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.CustomFoodId, request.UserId);
            if (result is null) return Errors.CustomFoods.NotFound;
            return result;
        }
    }
}
