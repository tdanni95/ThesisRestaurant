using Microsoft.EntityFrameworkCore;

namespace ThesisRestaurant.Infrastructure.Persistence
{
    public class ThesisRestaurantDbContext : DbContext
    {
        public ThesisRestaurantDbContext(DbContextOptions<ThesisRestaurantDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ThesisRestaurantDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
