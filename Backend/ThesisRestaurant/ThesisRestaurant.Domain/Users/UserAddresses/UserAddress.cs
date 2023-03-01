﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    }
}
