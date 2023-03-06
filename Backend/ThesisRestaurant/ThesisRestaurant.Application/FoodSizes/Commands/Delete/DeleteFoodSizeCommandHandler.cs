using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;

namespace ThesisRestaurant.Application.FoodSizes.Commands.Delete
{
    public class DeleteFoodSizeCommandHandler : IRequestHandler<DeleteFoodSizeCommand, ErrorOr<Deleted>>
    {
        private readonly IFoodSizeRepository _repository;
        public DeleteFoodSizeCommandHandler(IFoodSizeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteFoodSizeCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.Delete(request.Id);
            return result;
        }
    }
}
