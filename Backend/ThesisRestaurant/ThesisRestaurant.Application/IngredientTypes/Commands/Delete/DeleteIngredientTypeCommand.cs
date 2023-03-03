using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.IngredientTypes.Commands.Delete
{
    public record DeleteIngredientTypeCommand(int Id) : IRequest<ErrorOr<Deleted>>;
}
