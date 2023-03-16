using Mapster;
using ThesisRestaurant.Application.Users.Commands.ChangeUserPassword;
using ThesisRestaurant.Application.Users.Commands.Update;
using ThesisRestaurant.Application.Users.Commands.UpdateUserLevel;
using ThesisRestaurant.Contracts.User;
using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Api.Common.Mapping
{
    public class UserMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UpdateUser, UpdateUserCommand>();
            config.NewConfig<UpdateUserLevel, UpdateUserLevelCommand>();
            config.NewConfig<ChangeUserPassword, ChangeUserPasswordCommand>();

            config.NewConfig<User, UserResponse>()
                .Map(dest => dest.Addresses, src => src.UserAddresses)
                .Map(dest => dest, src => src);
        }
    }
}

