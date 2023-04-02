using FluentValidation;

namespace ThesisRestaurant.Application.Cart.Commands.Create
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.AddressId).NotEmpty().GreaterThan(0);

            RuleFor(x => x.CustomFoodIds)
                .Must(x => x != null && x.Count > 0)
                .When(x => x.OrderItems == null || x.OrderItems.Count == 0)
                .WithMessage("Either CustomFoods or OrderItems must contain at least one item.");

            RuleFor(x => x.OrderItems)
                .Must(x => x != null && x.Count > 0)
                .When(x => x.CustomFoodIds == null || x.CustomFoodIds.Count == 0)
                .WithMessage("Either CustomFoods or OrderItems must contain at least one item.");
        }
    }
}
