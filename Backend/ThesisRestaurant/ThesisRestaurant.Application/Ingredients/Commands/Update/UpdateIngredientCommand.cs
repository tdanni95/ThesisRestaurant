using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Application.Ingredients.Commands.Update
{
    public record UpdateIngredientCommand(int Id, string Name, double Price, int IngredientTypeId) : IRequest<ErrorOr<Ingredient>>;
}
