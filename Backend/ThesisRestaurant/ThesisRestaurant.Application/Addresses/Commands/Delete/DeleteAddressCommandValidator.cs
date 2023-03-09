using FluentValidation;

namespace ThesisRestaurant.Application.Addresses.Commands.Delete
{
    public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
    {
        public DeleteAddressCommandValidator()
        {
            RuleFor(x => x.userId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.addressId).NotEmpty().GreaterThan(0);
        }
    }
}
