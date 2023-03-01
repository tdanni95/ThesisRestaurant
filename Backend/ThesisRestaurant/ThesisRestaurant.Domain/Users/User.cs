using System.ComponentModel.DataAnnotations;
using ThesisRestaurant.Domain.CustomFoods;
using ThesisRestaurant.Domain.Orders;
using ThesisRestaurant.Domain.Users.UserAddresses;

namespace ThesisRestaurant.Domain.Users
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string FirstName { get; set; }
        [MaxLength(255)]
        public string LastName { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(255)]
        public string Password { get; set; }
        [MaxLength(255)]
        public string PhoneNumber { get; set; }
        public string? AuthToken { get; set; }
        public byte Level { get; set; }
        public List<UserAddress> UserAddresses { get; set; } = new();
        public List<CustomFood> CustomFoods { get; set; } = new();

    }
}
