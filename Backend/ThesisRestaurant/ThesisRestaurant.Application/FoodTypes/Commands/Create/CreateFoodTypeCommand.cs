using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Domain.FoodTypes;

namespace ThesisRestaurant.Application.FoodTypes.Commands.Create
{
    public record CreateFoodTypeCommand(string Name) : IRequest<ErrorOr<FoodType>>;

}
