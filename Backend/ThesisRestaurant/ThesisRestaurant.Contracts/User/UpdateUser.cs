using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Contracts.User
{
    public record UpdateUser(int Id,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber);
}
