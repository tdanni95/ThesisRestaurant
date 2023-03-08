using ErrorOr;
using MediatR;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Application.IngredientTypes.Commands.Create
{
    public record CreateIngredientTypeCommand(string Name, int MinOption, int MaxOption) : IRequest<ErrorOr<IngredientType>>;
}
