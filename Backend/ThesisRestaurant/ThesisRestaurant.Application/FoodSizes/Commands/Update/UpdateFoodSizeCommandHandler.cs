using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;

namespace ThesisRestaurant.Application.FoodSizes.Commands.Update
{
    public class UpdateFoodSizeCommandHandler : IRequestHandler<UpdateFoodSizeCommand, ErrorOr<FoodSize>>
    {
        private readonly IFoodSizeRepository _repository;
        private readonly IFoodTypeRepository _foodTypeRepository;
        public UpdateFoodSizeCommandHandler(IFoodSizeRepository repository, IFoodTypeRepository foodTypeRepository)
        {
            _repository = repository;
            _foodTypeRepository = foodTypeRepository;
        }

        public async Task<ErrorOr<FoodSize>> Handle(UpdateFoodSizeCommand request, CancellationToken cancellationToken)
        {
            var type = await _foodTypeRepository.GetById(request.FoodTypeId);
            if (type.IsError) return type.Errors;
            var size = FoodSize.Create(request.Name, request.Multiplier, type.Value, request.Id);

            var result = await _repository.Update(size);
            if (result.IsError) return result.Errors;
            return size;
        }
    }
}
