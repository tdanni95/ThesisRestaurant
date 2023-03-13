using FluentValidation;
using System.Globalization;

namespace ThesisRestaurant.Application.Foods.Commands.Discount
{
    public class AddDiscountCommandValidator : AbstractValidator<AddDiscountCommand>
    {
        public AddDiscountCommandValidator()
        {
            RuleFor(x => x.FoodId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
            RuleFor(x => x.From).Must(BeAValidDate).WithMessage("From date is not a valid date format");
            RuleFor(x => x.To).Must(BeAValidDate).WithMessage("From date is not a valid date format");
        }

        private bool BeAValidDate(string value)
        {
            // specify your desired date format here
            string dateFormat = "yyyy-MM-dd";

            // try to parse the string as a DateTime using the specified format
            return DateTime.TryParseExact(value, dateFormat, CultureInfo.InvariantCulture,
                                          DateTimeStyles.None, out _);
        }
    }
}
