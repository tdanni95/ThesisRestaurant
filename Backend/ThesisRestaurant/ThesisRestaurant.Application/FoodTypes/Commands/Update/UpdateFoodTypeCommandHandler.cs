using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.FoodTypes;

namespace ThesisRestaurant.Application.FoodTypes.Commands.Update
{
    public class UpdateFoodTypeCommandHandler : IRequestHandler<UpdateFoodTypeCommand, ErrorOr<FoodType>>
    {
        private readonly IFoodTypeRepository _foodTypeRepository;
        public UpdateFoodTypeCommandHandler(IFoodTypeRepository foodTypeRepository)
        {
            _foodTypeRepository = foodTypeRepository;
        }

        public async Task<ErrorOr<FoodType>> Handle(UpdateFoodTypeCommand request, CancellationToken cancellationToken)
        {
            var type = FoodType.Create(request.Name, request.Id);
            var result = await _foodTypeRepository.Update(type);
            if (result.IsError) return result.Errors;
            return type;
        }
    }
}
