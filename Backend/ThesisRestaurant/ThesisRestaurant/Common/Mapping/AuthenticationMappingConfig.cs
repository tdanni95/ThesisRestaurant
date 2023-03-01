using Mapster;
using ThesisRestaurant.Application.Authentication.Commands.Register;
using ThesisRestaurant.Application.Authentication.Common;
using ThesisRestaurant.Application.Authentication.Queries.Login;
using ThesisRestaurant.Contracts.Authentication;

namespace ThesisRestaurant.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginQuery>();
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}
