using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ThesisRestaurant.Domain.FoodTypes;
using ThesisRestaurant.Domain.Ingredients;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;
using ThesisRestaurant.Domain.Foods;
using ThesisRestaurant.Domain.Users;
using ThesisRestaurant.Domain.Users.RefreshTokens;
using ThesisRestaurant.Domain.Users.UserAddresses;
using ThesisRestaurant.Infrastructure.Authentication;
using ThesisRestaurant.Infrastructure.Persistence;

namespace ThesisRestaurant.Infrastructure
{
    public static class TaskInitializer
    {
        public static WebApplication Seed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<ThesisRestaurantDbContext>();
                var passwordHandler = new PasswordHandler();

                context.Database.EnsureCreated();
                var ingrediendType = new List<IngredientType>() {
                    IngredientType.Create("Pizza sauce", 1, 1),
                    IngredientType.Create("Meat", 0, 3),
                    IngredientType.Create("Vegetable", 0, 3),
                    IngredientType.Create("Other", 0, 3),
                    IngredientType.Create("Cheese", 1, 3),
                    IngredientType.Create("Side dish", 1, 1),
                    IngredientType.Create("Main dish", 1, 1),
                    IngredientType.Create("Hamburger meat", 0, 1),
                    IngredientType.Create("Burger sauce", 0, 2)
                };

                foreach (var item in ingrediendType)
                {
                    context.IngredientTypes.Add(item);
                }


                //sauces
                var TomatoSauce = Ingredient.Create("Tomato sauce", 200, ingrediendType[0]);
                var BBQ = Ingredient.Create("BBQ sauce", 200, ingrediendType[0]);
                var SourCreamSauce = Ingredient.Create("Sour cream sauce", 200, ingrediendType[0]);
                var Mustard = Ingredient.Create("Mustard sauce with sour cream", 200, ingrediendType[0]);
                var SourCreamGarlicSauce = Ingredient.Create("Sour cream sauce with garlic", 200, ingrediendType[0]);
                var Cream = Ingredient.Create("Cream cheese sauce", 200, ingrediendType[0]);
                //meats
                var Salami = Ingredient.Create("Salami", 250, ingrediendType[1]);
                var GrilledChicken = Ingredient.Create("Grilled chicken breast", 250, ingrediendType[1]);
                var Ham = Ingredient.Create("Ham", 250, ingrediendType[1]);
                var Bacon = Ingredient.Create("Bacon", 250, ingrediendType[1]);
                var Tuna = Ingredient.Create("Tuna", 300, ingrediendType[1]);
                var Sausages = Ingredient.Create("Sausages", 250, ingrediendType[1]);
                //veggies
                var Corn = Ingredient.Create("Corn", 150, ingrediendType[2]);
                var Tomato = Ingredient.Create("Tomato", 150, ingrediendType[2]);
                var Onion = Ingredient.Create("Onion", 150, ingrediendType[2]);
                var Cucumber = Ingredient.Create("Cucumber", 150, ingrediendType[2]);
                var Pineapple = Ingredient.Create("Pineapple", 150, ingrediendType[2]);
                var Broccoli = Ingredient.Create("Broccoli", 150, ingrediendType[2]);
                var Bean = Ingredient.Create("Bean", 150, ingrediendType[2]);
                var Garlic = Ingredient.Create("Garlic", 150, ingrediendType[2]);
                var Mushroom = Ingredient.Create("Mushroom", 150, ingrediendType[2]);
                var Paprika = Ingredient.Create("Paprika", 150, ingrediendType[2]);
                var Jalapeno = Ingredient.Create("Jalapeno", 150, ingrediendType[2]);
                var Olive = Ingredient.Create("Olive berry", 150, ingrediendType[2]);
                var Salad = Ingredient.Create("Salad", 150, ingrediendType[2]);
                //other
                var Egg = Ingredient.Create("Egg", 150, ingrediendType[3]);
                var SourCreamOther = Ingredient.Create("Sour cream", 150, ingrediendType[3]);
                var OnionJam = Ingredient.Create("Onion jam", 300, ingrediendType[3]);
                //cheese
                var Gouda = Ingredient.Create("Gouda", 200, ingrediendType[4]);
                var Feta = Ingredient.Create("Feta", 200, ingrediendType[4]);
                var Mozzarella = Ingredient.Create("Mozzarella", 200, ingrediendType[4]);
                var Cheddar = Ingredient.Create("Cheddar", 200, ingrediendType[4]);
                var Parmesan = Ingredient.Create("Parmesan", 200, ingrediendType[4]);
                var SmokedChees = Ingredient.Create("Smoked cheese", 200, ingrediendType[4]);
                //side dish
                var Fries = Ingredient.Create("Fries", 400, ingrediendType[5]);
                var Rice = Ingredient.Create("Rice", 400, ingrediendType[5]);
                var MashedPot = Ingredient.Create("Mashed potatoes", 400, ingrediendType[5]);
                var Cesar = Ingredient.Create("Cesar salad", 600, ingrediendType[5]);
                var BakedSweet = Ingredient.Create("Baked sweet potatoes", 600, ingrediendType[5]);
                //main dish
                var GrilledChiecken = Ingredient.Create("Grilled chicken", 700, ingrediendType[6]);
                var FriedCheese = Ingredient.Create("Fried cheese", 700, ingrediendType[6]);
                var CordonB = Ingredient.Create("Cordon bleu", 700, ingrediendType[6]);
                var GyrosMeat = Ingredient.Create("Gyros meat", 700, ingrediendType[6]);
                //Hamburger meat
                var BurgerMeat = Ingredient.Create("Burger meat", 850, ingrediendType[7]);
                var VeggyBurger = Ingredient.Create("Vegetarian burger", 850, ingrediendType[7]);
                //burger sauce
                var mustard = Ingredient.Create("Mustard", 850, ingrediendType[8]);
                var ketchup = Ingredient.Create("Ketchup", 850, ingrediendType[8]);

                var ingredients = new List<Ingredient>() { mustard, ketchup, Salad, OnionJam, TomatoSauce, BBQ, SourCreamSauce, Mustard, SourCreamGarlicSauce, Cream, Salami, GrilledChicken, Ham, Bacon, Tuna, Sausages, Corn, Tomato, Onion, Cucumber, Pineapple, Broccoli, Bean, Garlic, Mushroom, Paprika, Jalapeno, Olive, Egg, SourCreamOther, Gouda, Feta, Mozzarella, Cheddar, Parmesan, SmokedChees, Fries, Rice, MashedPot, Cesar, BakedSweet, GrilledChiecken, FriedCheese, CordonB, BurgerMeat, VeggyBurger };

                foreach (var item in ingredients)
                {
                    context.Ingredients.Add(item);
                }

                var foodTypes = new List<FoodType>()
                {
                    FoodType.Create("Pizza", 1200),
                    FoodType.Create("Hamburger", 800),
                    FoodType.Create("Dish", 1500),
                    FoodType.Create("Salad", 500),
                    FoodType.Create("Soup", 500),
                    FoodType.Create("Dessert", 500),
                };
                foreach (var item in foodTypes)
                {
                    context.FoodTypes.Add(item);
                }

                var foods = new List<Food>() {
                    //pizza
                    Food.Create("Cardinale", foodTypes[0], 1990, new List<Ingredient>() {TomatoSauce, Gouda, Ham, Corn}),
                    Food.Create("Salami", foodTypes[0], 1990, new List<Ingredient> {TomatoSauce, Gouda, Salami}),
                    Food.Create("Hawaii", foodTypes[0], 2090, new List<Ingredient> {TomatoSauce, Gouda, Ham, Corn, Pineapple}),
                    Food.Create("Mexico", foodTypes[0], 2200, new List<Ingredient>{TomatoSauce, Gouda, GrilledChicken, Bean, Corn, Jalapeno, Paprika}),
                    Food.Create("Viktoria", foodTypes[0], 2990, new List<Ingredient>() {BBQ, Cheddar, Feta, OnionJam, GrilledChicken, Bacon, Sausages, Egg, Pineapple, Olive}),
                    Food.Create("Provence", foodTypes[0], 2400, new List<Ingredient>() {Mustard, Gouda, GrilledChicken, Onion}),
                    //burger
                    Food.Create("CheeseBurger", foodTypes[1], 2000, new List<Ingredient>() {BurgerMeat, Cheddar, mustard, ketchup}),
                    Food.Create("VeggieBurger", foodTypes[1], 2000, new List<Ingredient>() {BurgerMeat, Cheddar, mustard, ketchup}),
                    //dishes
                    Food.Create("Stuffed bowl", foodTypes[2], 4000, new List<Ingredient>() {CordonB, FriedCheese, GrilledChicken, Fries, Rice}),
                    Food.Create("Gyros", foodTypes[2], 2500, new List<Ingredient>() {GyrosMeat, Cesar, Salad}),
                    //salad??
                    Food.Create("Cesar salad", foodTypes[2], 2000, new List<Ingredient>() {Cesar, GrilledChicken}),
                    //Soup
                    Food.Create("Goulash", foodTypes[3], 2500, new List<Ingredient>()),
                    //dessert
                    Food.Create("Cheese cake", foodTypes[4], 1500, new List<Ingredient>()),
                    Food.Create("Apple pie", foodTypes[4], 1500, new List<Ingredient>()),
                };

                foreach (var item in foods)
                {
                    context.Foods.Add(item);
                }

                string hashedPassword = passwordHandler.HashPassword("admin");
                var addresses = new List<UserAddress>() { UserAddress.Create(3021, "Lőrinci", "Pizza utca", "12 b") };
                var user = User.Create("admin", "admin", "admin@gmail.com", hashedPassword, "06-30-123-4567", new RefreshToken(), 3, addresses);
                context.Users.Add(user);
                context.SaveChanges();
            }

            return app;
        }
    }
}
