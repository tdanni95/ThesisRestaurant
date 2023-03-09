using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Contracts.User
{
    public record ChangeUserPassword(int Id, string OldPassword, string NewPassword, string NewPasswordAgain);
}
