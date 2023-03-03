using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;

namespace ThesisRestaurant.Application.IngredientTypes.Commands.Delete
{
    public class DeleteIngredientTypeCommandHandler : IRequestHandler<DeleteIngredientTypeCommand, ErrorOr<Deleted>>
    {
        private readonly IIngredientTypeRepository _repository;

        public DeleteIngredientTypeCommandHandler(IIngredientTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteIngredientTypeCommand request, CancellationToken cancellationToken)
        {
            return await _repository.Delete(request.Id);
        }
    }
}
