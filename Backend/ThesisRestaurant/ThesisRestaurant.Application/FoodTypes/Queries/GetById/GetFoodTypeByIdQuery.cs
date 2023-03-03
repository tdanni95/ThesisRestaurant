using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Domain.FoodTypes;

namespace ThesisRestaurant.Application.FoodTypes.Queries.GetById
{
    public record GetFoodTypeByIdQuery(int Id) : IRequest<ErrorOr<FoodType>>;    
}
