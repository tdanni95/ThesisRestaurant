using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.FoodTypes;

namespace ThesisRestaurant.Application.FoodTypes.Queries.GetById
{
    public class GetFoodTypeByIdQueryHandler : IRequestHandler<GetFoodTypeByIdQuery, ErrorOr<FoodType>>
    {
        private readonly IFoodTypeRepository _foodTypeRepository;
        public GetFoodTypeByIdQueryHandler(IFoodTypeRepository foodTypeRepository)
        {
            _foodTypeRepository = foodTypeRepository;
        }


        public async Task<ErrorOr<FoodType>> Handle(GetFoodTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _foodTypeRepository.GetById(request.Id);
        }
    }
}
