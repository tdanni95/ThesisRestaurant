using Mapster;
using ThesisRestaurant.Application.Users.Commands.Update;
using ThesisRestaurant.Contracts.User;

namespace ThesisRestaurant.Api.Common.Mapping
{
    public class UserMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UpdateUser, UpdateUserCommand>();
        }
    }
}

