using FluentValidation;

namespace ThesisRestaurant.Application.Addresses.Commands.Create
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.ZipCode).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.HouseNumber).NotEmpty();
        }
    }
}
