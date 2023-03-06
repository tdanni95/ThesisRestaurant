using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;

namespace ThesisRestaurant.Application.FoodSizes.Commands.Update
{
    public record UpdateFoodSizeCommand(int Id, string Name, double Multiplier, int FoodTypeId) : IRequest<ErrorOr<FoodSize>>;
}
