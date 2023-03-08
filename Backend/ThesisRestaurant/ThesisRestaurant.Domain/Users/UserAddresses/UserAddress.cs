using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Domain.Orders;

namespace ThesisRestaurant.Domain.Users.UserAddresses
{
    public class UserAddress
    {
        public int Id { get; set; }
        public int ZipCode { get; set; }
        [MaxLength(255)]
        public string City { get; set; }
        [MaxLength(500)]
        public string Street { get; set; }
        [MaxLength(100)]
        public string HouseNumber { get; set; }

        public List<Order> Orders { get; set; } = new();

        private UserAddress(int id, int zipCode, string city, string street, string houseNumber)
        {
            Id = id;
            ZipCode = zipCode;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
        }

        public static UserAddress Create(int zipCode, string city, string street, string houseNumber, int id = 0)
        {
            return new(id, zipCode, city, street, houseNumber);
        }

        public void Update(UserAddress address)
        {
            this.Id = address.Id;
            this.ZipCode = address.ZipCode;
            this.City = address.City;
            this.Street = address.Street;
            this.HouseNumber = address.HouseNumber;
        }

        public string GetFullAddress()
        {
            return $"{ZipCode}, {City} {Street} {HouseNumber}";
        }


    }
}
