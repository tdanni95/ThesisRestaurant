﻿using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Domain.FoodTypes;

namespace ThesisRestaurant.Application.FoodTypes.Queries.GetAll
{
    public record GetFoodTypesQuery : IRequest<ErrorOr<List<FoodType>>>;
    
}
