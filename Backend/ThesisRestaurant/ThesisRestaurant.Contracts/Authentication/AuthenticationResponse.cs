using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Contracts.Authentication
{
    public record AuthenticationResponse(
        int Id,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        string Token,
        string RefreshToken
     );
}
