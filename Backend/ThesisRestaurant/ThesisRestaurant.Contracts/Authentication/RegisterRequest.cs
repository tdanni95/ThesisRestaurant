using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Contracts.Authentication
{
    public record RegisterRequest(
            string FirstName, string LastName, string Email, string Password, string PhoneNumber, List<RegisterAddressRequest> Addresses
        );

    public record RegisterAddressRequest(
            int ZipCode, string City, string Street, string HouseNumber
        );
}
