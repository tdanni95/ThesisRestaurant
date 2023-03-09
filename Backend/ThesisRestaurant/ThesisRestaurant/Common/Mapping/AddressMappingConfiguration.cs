using Mapster;
using ThesisRestaurant.Application.Addresses.Commands.Create;
using ThesisRestaurant.Application.Addresses.Commands.Update;
using ThesisRestaurant.Contracts.Authentication;

namespace ThesisRestaurant.Api.Common.Mapping
{
    public class AddressMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(RegisterAddressRequest Request, int UserId), CreateAddressCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.Request);

            config.NewConfig<(RegisterAddressRequest Request, int UserId, int AddressId), UpdateAddressCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.AddressId, src => src.AddressId)
                .Map(dest => dest, src => src.Request);
        }
    }
}
