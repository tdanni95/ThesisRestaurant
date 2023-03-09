using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.FoodTypes;

namespace ThesisRestaurant.Application.FoodTypes.Commands.Create
{
    public class CreateFoodTypeCommandHandler : IRequestHandler<CreateFoodTypeCommand, ErrorOr<FoodType>>
    {
        private readonly IFoodTypeRepository _repository;
        public CreateFoodTypeCommandHandler(IFoodTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<FoodType>> Handle(CreateFoodTypeCommand request, CancellationToken cancellationToken)
        {
            var type = FoodType.Create(request.Name, request.Price);
            var result = await _repository.Add(type);
            if (result.IsError) return result.Errors;
            return type;
        }
    }
}
