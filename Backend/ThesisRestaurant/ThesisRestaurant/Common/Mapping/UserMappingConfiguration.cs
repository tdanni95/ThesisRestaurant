using Mapster;
using ThesisRestaurant.Application.Users.Commands.ChangeUserPassword;
using ThesisRestaurant.Application.Users.Commands.Update;
using ThesisRestaurant.Application.Users.Commands.UpdateUserLevel;
using ThesisRestaurant.Contracts.User;

namespace ThesisRestaurant.Api.Common.Mapping
{
    public class UserMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UpdateUser, UpdateUserCommand>();
            config.NewConfig<UpdateUserLevel, UpdateUserLevelCommand>();
            config.NewConfig<ChangeUserPassword, ChangeUserPasswordCommand>();
        }
    }
}

