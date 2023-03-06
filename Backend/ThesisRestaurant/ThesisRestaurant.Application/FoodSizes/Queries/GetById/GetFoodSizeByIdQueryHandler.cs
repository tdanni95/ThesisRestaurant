using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;

namespace ThesisRestaurant.Application.FoodSizes.Queries.GetById
{
    public class GetFoodSizeByIdQueryHandler : IRequestHandler<GetFoodSizeByIdQuery, ErrorOr<FoodSize>>
    {
        private readonly IFoodSizeRepository _repository;
        public GetFoodSizeByIdQueryHandler(IFoodSizeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<FoodSize>> Handle(GetFoodSizeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetById(request.Id);
        }
    }
}
