﻿using Mapster;
using ThesisRestaurant.Application.FoodTypes.Commands.Create;
using ThesisRestaurant.Application.FoodTypes.Commands.Update;
using ThesisRestaurant.Contracts.FoodType;
using ThesisRestaurant.Domain.FoodTypes;

namespace ThesisRestaurant.Api.Common.Mapping
{
    public class FoodTypeMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<FoodType, FoodTypeResponse>();

            config.NewConfig<CreateFoodTypeRequest, CreateFoodTypeCommand>();
            config.NewConfig<UpdateFoodTypeRequest, UpdateFoodTypeCommand>();
        }
    }
}
