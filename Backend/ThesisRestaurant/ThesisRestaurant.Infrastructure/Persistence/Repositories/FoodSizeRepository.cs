using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;

namespace ThesisRestaurant.Infrastructure.Persistence.Repositories
{
    public class FoodSizeRepository : IFoodSizeRepository
    {
        public Task<ErrorOr<Created>> Add(FoodSize ingredient)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<Deleted>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<List<FoodSize>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<List<FoodSize>>> GetAllWithIngredient()
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<FoodSize>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<Updated>> Update(FoodSize ingredient)
        {
            throw new NotImplementedException();
        }
    }
}
