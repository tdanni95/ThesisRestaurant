using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.Users.Commands.ChangeUserPassword
{
    public record ChangeUserPasswordCommand(int Id, string OldPassword, string NewPassword, string NewPasswordAgain) : IRequest<ErrorOr<Updated>>;
}
