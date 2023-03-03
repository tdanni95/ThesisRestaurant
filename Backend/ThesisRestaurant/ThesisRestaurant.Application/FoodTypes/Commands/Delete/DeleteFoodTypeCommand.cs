using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.FoodTypes.Commands.Delete
{
    public record DeleteFoodTypeCommand(int Id) : IRequest<ErrorOr<Deleted>>;
}
