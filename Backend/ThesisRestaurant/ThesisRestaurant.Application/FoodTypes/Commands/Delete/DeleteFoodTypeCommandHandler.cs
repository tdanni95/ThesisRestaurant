using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;

namespace ThesisRestaurant.Application.FoodTypes.Commands.Delete
{
    public class DeleteFoodTypeCommandHandler : IRequestHandler<DeleteFoodTypeCommand, ErrorOr<Deleted>>
    {
        private readonly IFoodTypeRepository _repository;

        public DeleteFoodTypeCommandHandler(IFoodTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteFoodTypeCommand request, CancellationToken cancellationToken)
        {
            return await _repository.Delete(request.Id);
        }
    }
}
