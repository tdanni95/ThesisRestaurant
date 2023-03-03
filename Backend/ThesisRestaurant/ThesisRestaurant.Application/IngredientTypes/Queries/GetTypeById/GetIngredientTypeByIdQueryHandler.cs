using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Application.IngredientTypes.Queries.GetTypeById
{
    public class GetIngredientTypeByIdQueryHandler : IRequestHandler<GetIngredientTypeByIdQuery, ErrorOr<IngredientType>>
    {
        private readonly IIngredientTypeRepository _repository;

        public GetIngredientTypeByIdQueryHandler(IIngredientTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<IngredientType>> Handle(GetIngredientTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetById(request.Id);
        }
    }
}
