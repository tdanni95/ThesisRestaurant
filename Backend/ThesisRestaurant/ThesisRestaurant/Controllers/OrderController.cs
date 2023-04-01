using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThesisRestaurant.Application.Cart.Commands.Create;
using ThesisRestaurant.Application.Cart.Queries;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Contracts.Orders;
using ThesisRestaurant.Domain.CustomFoods;
using ThesisRestaurant.Domain.Foods;
using ThesisRestaurant.Domain.Orders;
using ThesisRestaurant.Domain.Orders.OrderAdditionalIngredients;
using ThesisRestaurant.Domain.Orders.OrderCustomItems;
using ThesisRestaurant.Domain.Orders.OrderItems;
using ThesisRestaurant.Infrastructure.Persistence.Repositories;

namespace ThesisRestaurant.Api.Controllers
{
    [Route("order/{userId:int}")]
    [AllowAnonymous]
    public class OrderController : ApiController
    {

        private readonly ISender _meaditor;
        private readonly IMapper _mapper;

        public OrderController(ISender meaditor, IMapper mapper)
        {
            _meaditor = meaditor;
            _mapper = mapper;
        }

        [HttpPost("cart")]
        public async Task<IActionResult> GetItemsInCart(OrderRequest request)
        {
            var query = _mapper.Map<GetItemsInCartQuery>(request);
            var result = await _meaditor.Send(query);
            return result.Match(
                    items => Ok(_mapper.Map<CartResponse>(items)),
                    errors => Problem(errors)
                );
        }

        [HttpPost("{addressId:int}")]
        public async Task<IActionResult> PlaceOrder(OrderRequest order, int userId, int addressId)
        {
            var command = _mapper.Map<CreateOrderCommand>((order, userId, addressId));
            var result = await _meaditor.Send(command);

            return result.Match(
                    created => Ok(NoContent()),
                    errors => Problem(errors)
                );
        }

        //[HttpGet]
        //public async Task<IActionResult> GetUserOrders(int userId)
        //{

        //}

        //[HttpGet("all")]
        //public async Task<IActionResult> GetAllOrders()
        //{

        //}
        
    }
}
