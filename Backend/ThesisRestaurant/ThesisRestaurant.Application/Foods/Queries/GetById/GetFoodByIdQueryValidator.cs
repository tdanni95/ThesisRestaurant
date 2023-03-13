using FluentValidation;

namespace ThesisRestaurant.Application.Foods.Queries.GetById
{
    public class GetFoodByIdQueryValidator : AbstractValidator<GetFoodByIdQuery>
    {
        public GetFoodByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}
