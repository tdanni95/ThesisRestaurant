using FluentValidation;

namespace ThesisRestaurant.Application.CustomFoods.Queries.GetById
{
    public class GetCustomFoodByIdQueryValidator : AbstractValidator<GetCustomFoodByIdQuery>
    {
        public GetCustomFoodByIdQueryValidator()
        {
            RuleFor(x => x.CustomFoodId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.UserId).NotEmpty().GreaterThan(0);
        }
    }
}
