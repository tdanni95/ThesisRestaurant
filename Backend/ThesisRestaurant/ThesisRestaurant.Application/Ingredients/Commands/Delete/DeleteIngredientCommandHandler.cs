using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;

namespace ThesisRestaurant.Application.Ingredients.Commands.Delete
{
    public class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand, ErrorOr<Deleted>>
    {
        private readonly IIngredientRepository _repository;

        public DeleteIngredientCommandHandler(IIngredientRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.Delete(request.Id);

            return result;
        }
    }
}
