using FluentValidation;

namespace ThesisRestaurant.Application.Cart.Queries.UserOrders
{
    public class GetUserOrdersQueryValidator : AbstractValidator<GetUserOrdersQuery>
    {
        public GetUserOrdersQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}
