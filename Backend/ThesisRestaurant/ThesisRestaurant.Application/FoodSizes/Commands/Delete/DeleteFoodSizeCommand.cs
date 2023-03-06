using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.FoodSizes.Commands.Delete
{
    public record DeleteFoodSizeCommand(int Id) : IRequest<ErrorOr<Deleted>>;
}
