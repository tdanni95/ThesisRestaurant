using FluentValidation;

namespace ThesisRestaurant.Application.Addresses.Commands.Update
{
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.AddressId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.ZipCode).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.HouseNumber).NotEmpty();
        }
    }
}
