﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Contracts.FoodType
{
    public record CreateFoodTypeRequest(
            string Name,
            double Price
        );
}
