using Microsoft.EntityFrameworkCore;
using ThesisRestaurant.Domain.CustomFoods;
using ThesisRestaurant.Domain.Foods;
using ThesisRestaurant.Domain.Foods.FoodPictures;
using ThesisRestaurant.Domain.Foods.FoodPrices;
using ThesisRestaurant.Domain.FoodTypes;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;
using ThesisRestaurant.Domain.Ingredients;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;
using ThesisRestaurant.Domain.Orders;
using ThesisRestaurant.Domain.Orders.OrderCustomItems;
using ThesisRestaurant.Domain.Orders.OrderItems;
using ThesisRestaurant.Domain.Users;
using ThesisRestaurant.Domain.Users.UserAddresses;

namespace ThesisRestaurant.Infrastructure.Persistence
{
    public class ThesisRestaurantDbContext : DbContext
    {
        public ThesisRestaurantDbContext(DbContextOptions<ThesisRestaurantDbContext> options) : base(options)
        {

        }

        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        #endregion

        #region Ingredient
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientType> IngredientTypes { get; set; }
        #endregion

        #region Food
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<FoodPicture> FoodPictures { get; set; }
        public DbSet<FoodPrice> FoodPrices { get; set; }
        public DbSet<FoodSize> FoodSizes { get; set; }
        public DbSet<CustomFood> CustomFoods { get; set; }
        #endregion



        #region Order
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderCustomItem> OrderCustomItems { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ThesisRestaurantDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
