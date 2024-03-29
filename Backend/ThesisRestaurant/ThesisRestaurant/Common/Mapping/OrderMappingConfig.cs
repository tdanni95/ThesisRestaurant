﻿using Mapster;
using ThesisRestaurant.Application.Cart.Commands.Create;
using ThesisRestaurant.Application.Cart.Common;
using ThesisRestaurant.Application.Cart.Queries;
using ThesisRestaurant.Contracts.Ingredient;
using ThesisRestaurant.Contracts.Orders;
using ThesisRestaurant.Domain.Ingredients;
using ThesisRestaurant.Domain.Orders;
using ThesisRestaurant.Domain.Orders.OrderAdditionalIngredients;
using ThesisRestaurant.Domain.Orders.OrderCustomItems;
using ThesisRestaurant.Domain.Orders.OrderItems;

namespace ThesisRestaurant.Api.Common.Mapping
{
    public class OrderMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {

            config.NewConfig<GetItemsInCartQuery, OrderRequest>()
                .Map(dest => dest.CustomFoodIds, src => src.CustomFoodIds)
                .Map(dest => dest.OrderItems, src => src.OrderItems);

            config.NewConfig<Ingredient, OrderItemIngredientResponse>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.IngredientType, src => src.IngredientType.Name);

            config.NewConfig<OrderCustomItem, OrderCustomItemResponse>()
                .Map(dest => dest.Id, src => src.CustumFood.Id)
                .Map(dest => dest.Name, src => src.CustumFood.Name)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.FoodType, src => src.CustumFood.FoodType.Name)
                .Map(dest => dest.Ingredients, src => src.CustumFood.Ingredients)
                .Map(dest => dest.Quantity, src => src.Quantity);

            config.NewConfig<OrderAdditionalIngredient, OrderAdditionalIngredientResponse>()
                .Map(dest => dest.IngredientType, src => src.Ingredient.IngredientType.Name)
                .Map(dest => dest.Id, src => src.Ingredient.Id)
                .Map(dest => dest.Name, src => src.Ingredient.Name)
                .Map(dest => dest.Price, src => src.Ingredient.Price)
                .Map(dest => dest.Quantity, src => src.Quantity);

            config.NewConfig<OrderItem, OrderItemResponse>()
                .Map(dest => dest.Id, src => src.Food.Id)
                .Map(dest => dest.Name, src => src.Food.Name)
                .Map(dest => dest.FoodType, src => src.Food.Type.Name)
                .Map(dest => dest.FoodSizeId, src => src.FoodSize.Id)
                .Map(dest => dest.FoodSize, src => src.FoodSize.Name)
                .Map(dest => dest.FoodSizeMultiplier, src => src.FoodSize.Multiplier)
                .Map(dest => dest.Ingredients, src => src.Food.Ingredients)
                .Map(dest => dest.AdditionalIngredients, src => src.AdditionalIngredients)
                .Map(dest => dest.Quantity, src => src.Quantity)
                .Map(dest => dest.Price, src => src.Price);


            config.NewConfig<CartResult, CartResponse>()
                .Map(dest => dest.CustomFoods, src => src.CustomFoods)
                .Map(dest => dest.Foods, src => src.Foods);


            config.NewConfig<(OrderRequest request, int userId, int addressId), CreateOrderCommand>()
                .Map(dest => dest.AddressId, src => src.addressId)
                .Map(dest => dest.UserId, src => src.userId)
                .Map(dest => dest.CustomFoodIds, src => src.request.CustomFoodIds)
                .Map(dest => dest.OrderItems, src => src.request.OrderItems);

            config.NewConfig<Order, ActualOrderResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Created, src => src.Created)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.Items, src => src);

            config.NewConfig<OrderResult, OrderResponse>()
                .Map(dest => dest.User, src => src.User)
                .Map(dest => dest.Items, src => src.Order);
        }
    }
}
