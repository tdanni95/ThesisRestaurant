using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Application.IngredientTypes.Queries.GetTypes
{
    public class GetIngredientTypesQueryHandler : IRequestHandler<GetIngredientTypesQuery, ErrorOr<List<IngredientType>>>
    {
        private readonly IIngredientTypeRepository _repository;
        public GetIngredientTypesQueryHandler(IIngredientTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<List<IngredientType>>> Handle(GetIngredientTypesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll();
        }
    }
}
