using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Domain.Entities;

namespace ThesisRestaurant.Application.Authentication.Common
{
    public record AuthenticationResult(
            User User,
            string Token
        );
}
