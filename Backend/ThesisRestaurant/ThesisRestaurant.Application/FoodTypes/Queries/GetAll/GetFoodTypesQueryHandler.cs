using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.FoodTypes;

namespace ThesisRestaurant.Application.FoodTypes.Queries.GetAll
{
    public class GetFoodTypesQueryHandler : IRequestHandler<GetFoodTypesQuery, ErrorOr<List<FoodType>>>
    {
        IFoodTypeRepository _repostiory;

        public GetFoodTypesQueryHandler(IFoodTypeRepository repostiory)
        {
            _repostiory = repostiory;
        }

        public async Task<ErrorOr<List<FoodType>>> Handle(GetFoodTypesQuery request, CancellationToken cancellationToken)
        {
            return await _repostiory.GetAll();
        }
    }
}
