using Mapster;
using Project.Application.Authentication.Commands.Register;
using Project.Application.Authentication.Common;
using Project.Application.Authentication.Queries.Login;
using Project.Contracts.Authentication;

namespace Project.Application.Mapping;
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