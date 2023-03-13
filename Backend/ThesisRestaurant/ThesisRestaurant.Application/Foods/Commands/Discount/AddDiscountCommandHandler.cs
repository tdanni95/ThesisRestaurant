using ErrorOr;
using MediatR;
using System.Globalization;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Application.Foods.Common;
using ThesisRestaurant.Domain.Foods.FoodPrices;

namespace ThesisRestaurant.Application.Foods.Commands.Discount
{
    public class AddDiscountCommandHandler : IRequestHandler<AddDiscountCommand, ErrorOr<FoodResult>>
    {
        private readonly IFoodRepository _foodRepository;
        public AddDiscountCommandHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<ErrorOr<FoodResult>> Handle(AddDiscountCommand request, CancellationToken cancellationToken)
        {
            string format = "yyyy-MM-dd";
            DateTime from;
            DateTime to;
            DateTime.TryParseExact(request.From, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out from);
            DateTime.TryParseExact(request.To, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out to);
            var discount = FoodPrice.Create(request.Price, from, to);
            var result = await _foodRepository.AddDiscount(discount, request.FoodId);
            if (result.IsError) return result.Errors;
            var currentPrize = result.Value.GetCurrentPrice();

            return new FoodResult( currentPrize is not null ? currentPrize.Price : double.MaxValue, result.Value);
        }
    }
}
