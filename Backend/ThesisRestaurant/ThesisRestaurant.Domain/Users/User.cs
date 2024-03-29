﻿using System.ComponentModel.DataAnnotations;
using ThesisRestaurant.Domain.CustomFoods;
using ThesisRestaurant.Domain.Orders;
using ThesisRestaurant.Domain.Users.RefreshTokens;
using ThesisRestaurant.Domain.Users.UserAddresses;

namespace ThesisRestaurant.Domain.Users
{
    public class User
    {
        public int Id { get; private set; }
        [MaxLength(255)]
        public string FirstName { get; private set; }
        [MaxLength(255)]
        public string LastName { get; private set; }
        [MaxLength(255)]
        public string Email { get; private set; }
        [MaxLength(255)]
        public string Password { get; private set; }
        [MaxLength(255)]
        public string PhoneNumber { get; private set; }
        public RefreshToken? RefreshToken { get; private set; }
        public byte Level { get; private set; }
        public List<UserAddress> UserAddresses { get; private set; } = new();
        public List<CustomFood> CustomFoods { get; private set; } = new();

        public List<Order> Orders { get; private set; } = new();

        private User(int id, string firstName, string lastName, string email, string password, string phoneNumber,  byte level)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            Level = level;
        }

        private User(int id, string firstName, string lastName, string email, string password, string phoneNumber, RefreshToken? refreshToken, byte level, List<UserAddress> userAddresses)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            RefreshToken = refreshToken;
            Level = level;
            UserAddresses = userAddresses;
        }

        public static User Create(string firstName, string lastName, string email, string password, string phoneNumber, RefreshToken? refreshToken, byte level, List<UserAddress> userAddresses, int id = 0)
        {
            return new(id, firstName, lastName, email, password, phoneNumber, refreshToken, level, userAddresses);
        }

        public static User Create(string firstName, string lastName, string email, string phoneNumber, int id = 0)
        {
            return new User(id, firstName, lastName, email, "", phoneNumber, 1);
        }

        public void Update(User user)
        {
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.Password = user.Password;
            this.PhoneNumber = user.PhoneNumber;
            this.RefreshToken= user.RefreshToken;
            this.Level = user.Level;
            this.UserAddresses = user.UserAddresses;
        }

        public void UpdateBaseFields(string firstName, string lastname, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastname;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void SetLevel(byte level)
        {
            this.Level = level;
        }

        public void SetPassword(string password)
        {
            this.Password = password;
        }

        public void SetAuthToken(RefreshToken? token)
        {
            this.RefreshToken= token;
        }
    }
}
