using FluentValidation;

namespace ThesisRestaurant.Application.Foods.Commands.DeleteDiscount
{
    public class DeleteDiscountCommadValidator : AbstractValidator<DeleteDiscountCommad>
    {
        public DeleteDiscountCommadValidator()
        {
            RuleFor(x => x.id).NotEmpty().GreaterThan(0);
        }
    }
}
