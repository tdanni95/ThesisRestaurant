using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;

namespace ThesisRestaurant.Application.FoodSizes.Queries.GetAll
{
    public record GetAllFoodSizeQuery : IRequest<ErrorOr<List<FoodSize>>>;
}
