﻿using ErrorOr;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IIngredientTypeRepository
    {
        Task<ErrorOr<IngredientType>> GetById(int id);
        
        Task<ErrorOr<List<IngredientType>>> GetAll();
        Task<ErrorOr<List<IngredientType>>> GetAllWithIngredient();

        Task<ErrorOr<Created>> Add(IngredientType ingredient);
        Task<ErrorOr<Updated>> Update(IngredientType ingredient);
        Task<ErrorOr<Deleted>> Delete(int id);
    }
}
