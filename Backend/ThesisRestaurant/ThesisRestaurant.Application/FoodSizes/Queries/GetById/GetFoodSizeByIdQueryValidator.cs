using FluentValidation;

namespace ThesisRestaurant.Application.FoodSizes.Queries.GetById
{
    public class GetFoodSizeByIdQueryValidator : AbstractValidator<GetFoodSizeByIdQuery>
    {
        public GetFoodSizeByIdQueryValidator()
        {
            RuleFor(c => c.Id).NotEmpty().GreaterThan(0);
        }
    }
}
