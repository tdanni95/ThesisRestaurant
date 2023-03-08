using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.Common.Interfaces.Authentication
{
    public interface IPasswordHandler
    {
        string HashPassword(string passWord);
        bool VerifyPassword(string givenPassword, string actualPassword);
    }
}
