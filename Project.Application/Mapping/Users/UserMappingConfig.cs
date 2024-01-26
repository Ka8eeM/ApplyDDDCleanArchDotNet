using Mapster;
using Project.Application.Users.Commands.UpdateUser;
using Project.Application.Users.Commons;
using Project.Application.Users.Queries.GetAllUsers;
using Project.Contracts.Users.GetAllUsers;
using Project.Contracts.Users.UpdateUser;
using Project.Domain.UserAggregate;

namespace Project.Application.Mapping.Users;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(UpdateUserRequest Request, Guid UserId), UpdateUserCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<GetAllUsersRequest, GetAllUsersQuery>();

        config.NewConfig<User, UserDto>();
    }
}
