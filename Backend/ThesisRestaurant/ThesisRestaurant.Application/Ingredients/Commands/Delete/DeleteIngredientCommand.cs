using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.Ingredients.Commands.Delete
{
    public record DeleteIngredientCommand(int Id) : IRequest<ErrorOr<Deleted>>;
}
