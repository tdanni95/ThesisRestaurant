using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Application.Ingredients.Queries.GetById
{
    public record class GetIngredientByIdQuery(int Id) : IRequest<ErrorOr<Ingredient>>;
}
