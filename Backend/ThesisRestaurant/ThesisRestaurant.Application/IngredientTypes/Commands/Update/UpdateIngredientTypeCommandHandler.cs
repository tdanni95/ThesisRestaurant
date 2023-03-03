using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Application.IngredientTypes.Commands.Update
{
    internal class UpdateIngredientTypeCommandHandler : IRequestHandler<UpdateIngredientTypeCommand, ErrorOr<IngredientType>>
    {
        private readonly IIngredientTypeRepository _repository;
        public UpdateIngredientTypeCommandHandler(IIngredientTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<IngredientType>> Handle(UpdateIngredientTypeCommand request, CancellationToken cancellationToken)
        {
            var type = IngredientType.Create(request.Name, request.Id);

            var result = await _repository.Update(type);
            if (result.IsError) return result.Errors;

            return type;
        }
    }
}
