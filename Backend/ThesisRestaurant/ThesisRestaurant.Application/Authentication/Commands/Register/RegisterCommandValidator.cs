using FluentValidation;

namespace ThesisRestaurant.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Addresses).NotEmpty();
            RuleForEach(x => x.Addresses).SetValidator(new AddressValidator());
        }
    }

    public class AddressValidator : AbstractValidator<RegisterAddressCommand>
    {
        public AddressValidator()
        {
            RuleFor(x => x.ZipCode).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.HouseNumber).NotEmpty();
        }
    }
}
