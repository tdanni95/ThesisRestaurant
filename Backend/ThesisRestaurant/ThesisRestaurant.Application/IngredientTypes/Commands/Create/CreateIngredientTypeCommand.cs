using ErrorOr;
using MediatR;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Application.IngredientTypes.Commands.Create
{
    public record CreateIngredientTypeCommand(string Name) : IRequest<ErrorOr<IngredientType>>;
}
