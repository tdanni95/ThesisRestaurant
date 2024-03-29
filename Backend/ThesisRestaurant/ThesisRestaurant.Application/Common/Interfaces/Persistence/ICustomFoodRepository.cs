﻿using ErrorOr;
using ThesisRestaurant.Domain.CustomFoods;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface ICustomFoodRepository
    {
        public Task<CustomFood?> GetById(int id, int userId);
        public Task<List<CustomFood>> GetUserCustomFoods(int userId);

        public Task<List<CustomFood>> GetWhereidIn(List<int> ids);

        public Task<ErrorOr<Created>> AddCustomFood(CustomFood customFood, int userId);
        public Task<ErrorOr<Updated>> UpdateCustomFood(CustomFood customFood, int userId);
    }
}
